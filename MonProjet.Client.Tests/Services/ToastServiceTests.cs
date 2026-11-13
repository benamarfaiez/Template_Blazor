using MonProjet.Client.Models.UI.Feedback;
using MonProjet.Client.Services;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class ToastServiceTests
{
    [Fact]
    public void ShowSuccess_RaisesOnShowWithSuccessMessage()
    {
        var service = new ToastService();
        ToastMessage? received = null;
        service.OnShow += message => received = message;

        service.ShowSuccess("Saved", "Done");

        Assert.NotNull(received);
        Assert.Equal("Saved", received.Message);
        Assert.Equal("Done", received.Title);
        Assert.Equal(ToastSeverity.Success, received.Severity);
    }

    [Fact]
    public void Remove_RaisesOnRemoveWithToastId()
    {
        var service = new ToastService();
        var id = Guid.NewGuid();
        Guid? removedId = null;
        service.OnRemove += value => removedId = value;

        service.Remove(id);

        Assert.Equal(id, removedId);
    }
}
