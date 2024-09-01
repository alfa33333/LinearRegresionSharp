using Apache.Arrow;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Data.Analysis;  

namespace LinearRegression;

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


    public float Predict(float[] x)
    {
        return _Coefficients.DotProduct(Vector<float>.Build.DenseOfArray(x)) + Intercept;
    }


}
