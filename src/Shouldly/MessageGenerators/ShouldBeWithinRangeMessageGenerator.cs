using Shouldly.DifferenceHighlighting;

namespace Shouldly.MessageGenerators;

class ShouldBeWithinRangeMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        (context.ShouldMethod.StartsWith("ShouldBe", StringComparison.Ordinal) || context.ShouldMethod.StartsWith("ShouldNotBe", StringComparison.Ordinal))
        && !context.ShouldMethod.Contains("Contain")
        && context.Tolerance != null;

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var codePart = context.CodePart;
        var tolerance = context.Tolerance.ToStringAwesomely();
        var expected = context.Expected.ToStringAwesomely();
        var actualValue = context.Actual.ToStringAwesomely();
        string actual;
        if (codePart == actualValue)
        {
            actual = " not";
        }
        else
        {
            actual = $"""

                      {actualValue}
                      """;
        }

        var negated = context.ShouldMethod.Contains("Not") ? "not " : string.Empty;

        var message =
            $"""
             {codePart}
                 should {negated}be within
             {tolerance}
                 of
             {expected}
                 but was{actual}
             """;

        if (DifferenceHighlighter.CanHighlightDifferences(context))
        {
            message += $"""
                        
                            difference
                        {DifferenceHighlighter.HighlightDifferences(context)}
                        """;
        }

        return message;
    }
}