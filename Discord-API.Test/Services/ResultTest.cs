using API.Services;

namespace API.Test.Services;

public class ResultTest
{
    private class Success
    {
        public string Name => nameof(Success);
    }

    private class Error
    {
        public string Name => nameof(Error);
    }

    [Fact]
    public void WhenResultGetsSuccessThenValueShouldBeFilled()
    {
        Result<Success, Error> result = new Success();

        Assert.NotNull(result.Value);
        Assert.Equal(nameof(Success), result.Value.Name);
    }

    [Fact]
    public void WhenResultGetsErrorThenValueShouldBeNull()
    {
        Result<Success, Error> result = new Error();

        Assert.Null(result.Value);
    }

    [Fact]
    public void WhenResultGetsSuccessThenIsSuccessShouldBeTrue()
    {
        Result<Success, Error> result = new Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsError);
    }

    [Fact]
    public void WhenResultGetsErrorThenIsErrorShouldBeFalse()
    {
        Result<Success, Error> result = new Error();

        Assert.True(result.IsError);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void WhenResultGetsSuccessThenMatchShouldCallSuccessionAction()
    {
        Result<Success, Error> result = new Success();

        var callSuccess = result.Match(success =>
        {
            return success.Name;
        }, failed =>
        {
            Assert.Fail("The fail Action should not be called!");
            return failed.Name;
        });

        Assert.Equal(nameof(Success), callSuccess);
    }

    [Fact]
    public void WhenResultGetsErrorThenMatchShouldCallFailedAction()
    {
        Result<Success, Error> result = new Error();

        var callFailed = result.Match(success =>
        {
            Assert.Fail("The success Action should not be called!");
            return success.Name;
        }, failed =>
        {
            return failed.Name;
        });

        Assert.Equal(nameof(Error), callFailed);
    }
}