using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using Moq;
using SportsResultsNotifier.TerrenceLGee.Services;
using SportsResultsNotifier.TerrenceLGee.Interfaces;
using SportsResultsNotifier.TerrenceLGee.Tests.Resources;
using SportsResultsNotifier.TerrenceLGee.Data;

namespace SportsResultsNotifier.TerrenceLGee.Tests;

public class EmailServiceTests
{
    private readonly OptionsWrapper<EmailConfiguration> _configWrapper;
    private readonly Mock<ISmtpClientFactory> _mockClientFactory;
    private readonly IEmailService _emailService;

    public EmailServiceTests()
    {
        _configWrapper = new OptionsWrapper<EmailConfiguration>(EmailResources.GetConfiguration());
        _mockClientFactory = new Mock<ISmtpClientFactory>();
        _emailService = new EmailService(_configWrapper, _mockClientFactory.Object);
    }

    [Fact]
    public async Task SendEmailAsync_Does_Not_Throw_ExceptionWhenSentSuccessfully()
    {
        var emailData = EmailResources.GetEmailData();

        var mockClient = new Mock<ISmtpClient>();

        _mockClientFactory
            .Setup(c => c.Create())
            .Returns(mockClient.Object);

        mockClient
            .Setup(c => c.ConnectAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }
}
