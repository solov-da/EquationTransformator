using System;
using EquationTransformator.Core.Handlers;
using EquationTransformator.Implementation.Handlers;
using Xunit;

namespace EquationTransformator.Tests;

public class DefaultCanonicalEquationHandlerTests
{
    private readonly ICanonicalEquationHandler _canonicalEquationHandler;

    public DefaultCanonicalEquationHandlerTests()
    {
        _canonicalEquationHandler = new DefaultCanonicalEquationHandler();
    }

    [Theory]
    [InlineData("-(x+y)(x-y)", " - x^2 + y^2 = 0")]
    [InlineData("---2(-x+y) = 2(y-3)z", "2x - 2y - 2yz + 6z = 0")]
    [InlineData("---0(-x+y) = 2(y-3)z", " - 2yz + 6z = 0")]
    [InlineData("---0(-x+y) = 2((-1)(y-3)(-1))z", " - 2yz + 6z = 0")]
    [InlineData("xxx + y^2 = (yy + 1)(-2 + z)", "x^3 + 3y^2 - y^2z - z + 2 = 0")]
    [InlineData("x^5 + y^2 = yy5", "x^5 - 4y^2 = 0")]
    public void ProcessingValidEquations(string equation, string canonicalEquation)
    {
        Assert.Equal(canonicalEquation, _canonicalEquationHandler.Processing(equation).ToString());
    }


    [Theory]
    [InlineData("-(x+y) (x-y)")]
    [InlineData("---2.(-x+y) = 2(y-3)z")]
    [InlineData("---.0(-x+y) = 2(y-3)z")]
    [InlineData("---0(-x+y)^2 = ((-1)(y-3)(-1))z")]
    [InlineData("xxx + y^2 = (y y + 1)(-2 + z)")]
    [InlineData("x^5 + y^2 = y y5")]
    public void ProcessingInvalidEquations(string equation)
    {
        Assert.Throws<ArgumentException>(() => { _canonicalEquationHandler.Processing(equation); });
    }
}