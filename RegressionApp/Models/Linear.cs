using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Data.Analysis;

namespace RegressionApp.Models;

public class LinearModel
{
    public Matrix<float> Predictors { get; set; } = Matrix<float>.Build.DenseIdentity(1);
    public Vector<float> Response { get; set; } = Vector<float>.Build.Dense(1);
    private Vector<float> _Coefficients = Vector<float>.Build.Dense(1);
    public float Intercept { get; set; }

    public float[] Coefficients => [.. _Coefficients];

    public void LoadData(float[] predictorsInput, float[] responseInput)
    {
        Predictors = Matrix<float>.Build.DenseOfColumnArrays(predictorsInput);
        Response = Vector<float>.Build.Dense(responseInput);
    }

    public void LoadData(float[,] predictorsInput, float[] responseInput)
    {
        Predictors = Matrix<float>.Build.DenseOfArray(predictorsInput);
        Response = Vector<float>.Build.Dense(responseInput);
    }

    public void LoadData(DataFrame dataFrame, string responseName)
    {
        var responseIndex = dataFrame.Columns.Select(column => column.Name).ToList().IndexOf(responseName);
        Response = Vector<float>.Build.Dense(dataFrame.Columns[responseIndex].Cast<float>().ToArray());
        var datapred = dataFrame.Columns.Where(column => column.Name != responseName).Select(column => column.Cast<float>().ToArray()).ToArray();
        Predictors = Matrix<float>.Build.DenseOfColumnArrays(datapred);
    }


    public void Fit()
    {
        var designMatrix = Matrix<float>.Build.Dense(Predictors.RowCount, 1 + Predictors.ColumnCount);
        designMatrix.SetSubMatrix(0, 1, Predictors);
        designMatrix.SetColumn(0, Generate.Repeat(Predictors.RowCount, 1.0f));
        _Coefficients = designMatrix.TransposeThisAndMultiply(designMatrix).Cholesky().Solve(designMatrix.TransposeThisAndMultiply(Response));
    }


    public float[] Predict(float[] x, bool intercept = false)
    {
        if (intercept)
        {
            var designMatrix = Matrix<float>.Build.Dense(x.Length, Predictors.ColumnCount);
            designMatrix.SetSubMatrix(0, 0, Matrix<float>.Build.DenseOfColumnArrays(x));
            return designMatrix.Multiply(_Coefficients).ToArray();
        }
        else
        {
            var designMatrix = Matrix<float>.Build.Dense(x.Length, 1 + Predictors.ColumnCount);
            designMatrix.SetSubMatrix(0, 1, Matrix<float>.Build.DenseOfColumnArrays(x));
            designMatrix.SetColumn(0, Generate.Repeat(x.Length, 1.0f));
            return designMatrix.Multiply(_Coefficients).ToArray();
        }
    }
}
