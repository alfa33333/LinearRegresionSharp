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
        var result = Predictors.TransposeThisAndMultiply(Predictors).Cholesky().Solve(Predictors.TransposeThisAndMultiply(Response));
    }


    public float Predict(float[] x)
    {
        return Coefficients.DotProduct(Vector<float>.Build.DenseOfArray(x)) + Intercept;
    }

}