namespace Shouldly.Tests.ShouldNotThrow;

public class TaskScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void TaskScenarioShouldFail()
    {
        var task = Task.Run(() => throw new RankException(), TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
                task.ShouldNotThrow("Some additional context"),

            errorWithSource:
            """
            Task `task`
                should not throw but threw
            System.RankException
                with message
            "Attempted to operate on an array with the incorrect number of dimensions."

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should not throw but threw
            System.RankException
                with message
            "Attempted to operate on an array with the incorrect number of dimensions."

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => { }, TestContext.Current.CancellationToken);

        task.ShouldNotThrow();
    }
}