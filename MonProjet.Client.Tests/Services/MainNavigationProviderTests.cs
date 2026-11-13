using Microsoft.Extensions.Localization;
using MonProjet.Client.Resources;
using MonProjet.Client.Services.Navigation;
using NSubstitute;
using Xunit;

namespace MonProjet.Client.Tests.Services;

[Trait("Category", "Unit")]
public sealed class MainNavigationProviderTests
{
    [Fact]
    public void CreateLinks_ReturnsPrimaryNavigationLinks()
    {
        var localizer = Substitute.For<IStringLocalizer<SharedResource>>();
        localizer[Arg.Any<string>()].Returns(call =>
        {
            var key = call.Arg<string>();
            return new LocalizedString(key, $"localized:{key}");
        });
        var provider = new MainNavigationProvider(localizer);

        var links = provider.CreateLinks();

        Assert.Equal(4, links.Count);
        Assert.Contains(links, link => link.Id == "users" && link.Href == "/users");
        Assert.Contains(links, link => link.Id == "gallery" && link.Href == "/gallery");

        var platformLink = links.Single(link => link.Id == "platform");
        var treasury = Assert.Single(
            (platformLink.Groups ?? []).SelectMany(group => group.Items),
            item => item.Href == "/treasury/transactions");

        Assert.Equal("localized:Nav_Treasury_History", treasury.Title);
    }
}