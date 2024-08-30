using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace LinearRegression;

public class LinearModel
{
    public Matrix<float> Predictors { get; set; } = Matrix<float>.Build.DenseIdentity(1);
    public Vector<float> Response{ get; set; } = Vector<float>.Build.Dense(1);
    public Vector<float> Coefficients { get; set; } = Vector<float>.Build.Dense(1);
    public float Intercept { get; set; }

    public void loadData(float[] predictorsInput, float[] responseInput)
    {
        Predictors = Matrix<float>.Build.DenseOfColumnArrays(predictorsInput);
        Response = Vector<float>.Build.Dense(responseInput);
    }

    public void loadData(float[,] predictorsInput, float[] responseInput)
    {
        Predictors = Matrix<float>.Build.DenseOfArray(predictorsInput);
        Response = Vector<float>.Build.Dense(responseInput);
    }

    public void Fit()
    {
        var designMatrix = Matrix<float>.Build.Dense(Predictors.RowCount, 1 + Predictors.ColumnCount);
        designMatrix.SetSubMatrix(0, 1, Predictors);
        designMatrix.SetColumn(0, Generate.Repeat(Predictors.RowCount, 1.0f));
        Coefficients = designMatrix.TransposeThisAndMultiply(designMatrix).Cholesky().Solve(designMatrix.TransposeThisAndMultiply(Response));
    }


    public float Predict(float[] x)
    {
        return Coefficients.DotProduct(Vector<float>.Build.DenseOfArray(x)) + Intercept;
    }

}