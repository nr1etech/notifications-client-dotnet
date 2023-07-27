using ManageClient;
using ManageClient.Models;
using ManageClient.Models.Account;
using ManageClient.Models.Block;
using ManageClient.Models.Message;
using ManageClient.Models.Organization;
using ManageClient.Models.Sender;
using ManageClient.Models.Template;
using MessageClient;
using MessageClient.Models;
using ManageOidcClientCredentialsConfiguration = ManageClient.OidcClientCredentialsConfiguration;
using MessageOidcClientCredentialsConfiguration = MessageClient.OidcClientCredentialsConfiguration;

namespace NotificationsClientTest;

public static class ManageTest
{
    private static string GenerateUniqueSlug(string slug)
    {
        return $"{slug}-{Guid.NewGuid():D}";
    }

    public static async Task Test(
        string clientID,
        string clientSecret,
        string scope,
        string tokenUrl,
        string baseApiUrl,
        bool enableRequestLogging)
    {
        // SETUP
        var manageAuthConfig = new ManageOidcClientCredentialsConfiguration()
        {
            ClientID = clientID,
            ClientSecret = clientSecret,
            Scope = scope,
            TokenUrl = tokenUrl
        };

        DelegatingHandler manageAuthHandler = enableRequestLogging ?
            new LoggingManageOidcClientCredentialAuthHeaderHandler(manageAuthConfig, new HttpClient()) :
            new ManageClient.OidcClientCredentialAuthHeaderHandler(manageAuthConfig, new HttpClient());

        var httpClient = new HttpClient(manageAuthHandler)
        {
            BaseAddress = new Uri(baseApiUrl)
        };

        var manager = new NotificationsManageClient(httpClient);

        Organization? organization = null;
        Account? account = null;
        Template? emailTemplate = null;
        Template? smsTemplate = null;
        Sender? emailSender = null;
        Sender? smsSender = null;
        Block? emailBlock = null;
        Block? smsBlock = null;

        CreateMessageResult? message1 = null;
        CreateMessageResult? message2 = null;
        SendResponse? message3 = null;
        SendResponse? message4 = null;

        try
        {
            Console.WriteLine("🟩 Calling GetInfoAsync() to verify client connectivity and authentication.");
            var info = await manager.GetInfoAsync();

            if (
                info == null ||
                info.Accounts.Count == 0 ||
                string.IsNullOrEmpty(info.Accounts[0].AccountID) ||
                string.IsNullOrEmpty(info.Accounts[0].AccountDescription) ||
                string.IsNullOrEmpty(info.Accounts[0].AccountRole) ||
                string.IsNullOrEmpty(info.Accounts[0].OrganizationID) ||
                string.IsNullOrEmpty(info.Accounts[0].OrganizationName) ||
                string.IsNullOrEmpty(info.Accounts[0].OrganizationSlug)
               )
            {
                throw new ApplicationException("GetInfoAsync did not have the expected result");
            }

            // ***** Organization tests *****
            Console.WriteLine("🟩 Organization Tests");
            var createOrg = new CreateOrganizationData { OrganizationName = "Sanity Check", Slug = GenerateUniqueSlug("sanity-check") };
            organization = await manager.CreateOrganizationAsync(createOrg);
            var reOrganization = await manager.GetOrganizationAsync(organization.OrganizationID);
            if (
                createOrg.Slug != organization.Slug ||
                createOrg.OrganizationName != organization.OrganizationName ||
                string.IsNullOrEmpty(organization.OrganizationID) ||
                string.IsNullOrEmpty(organization.Status) ||
                organization.OrganizationID != reOrganization.OrganizationID ||
                organization.OrganizationName != reOrganization.OrganizationName ||
                organization.Slug != reOrganization.Slug ||
                organization.Status  != reOrganization.Status
                )
            {
                throw new ApplicationException("Create organization/Get Organization did not have the expected result");
            }

            var updateOrgData = new UpdateOrganizationData { OrganizationName = "Sanity Check Update" };
            var updatedOrganization = await manager.UpdateOrganizationAsync(organization.OrganizationID, updateOrgData);
            reOrganization = await manager.GetOrganizationAsync(organization.OrganizationID);
            if (
                updateOrgData.OrganizationName != updatedOrganization.OrganizationName ||
                updatedOrganization.OrganizationName != reOrganization.OrganizationName ||
                updatedOrganization.OrganizationID != organization.OrganizationID ||
                updatedOrganization.Slug != organization.Slug ||
                updatedOrganization.Status != organization.Status
            )
            {
                throw new ApplicationException("Update organization/Get Organization did not have the expected result");
            }

            var organizationList = await manager.GetOrganizationsAsync(1);
            var fOrg = organizationList.Results.FirstOrDefault();
            if (
                fOrg == null ||
                string.IsNullOrEmpty(fOrg.OrganizationName) ||
                string.IsNullOrEmpty(fOrg.OrganizationID) ||
                string.IsNullOrEmpty(fOrg.Slug) ||
                string.IsNullOrEmpty(fOrg.Status)
            )
            {
                throw new ApplicationException("Get Organization List did not have the expected result");
            }

            // Set the organization going forward so all the records are created under the test organization
            manager.OrganizationID = organization.OrganizationID;

            // ***** Account tests *****
            Console.WriteLine("🟩 Account Tests");

            var createAccountData = new CreateAccountData()
            {
                Description = "Sanity Check",
                UserName = "sanity@example.com",
                AccountType = AccountType.User,
                Role = AccountRole.Messaging,
            };
            account = await manager.CreateAccountAsync(createAccountData);
            var reAccount = await manager.GetAccountAsync(account.AccountID);

            if (
                createAccountData.AccountType != account.AccountType ||
                createAccountData.Description != account.Description ||
                createAccountData.Role != account.Role ||
                createAccountData.UserName != account.UserName ||
                account.AccountID != reAccount.AccountID ||
                account.AccountType != reAccount.AccountType ||
                account.Description != reAccount.Description ||
                account.Identity != reAccount.Identity ||
                account.OrganizationName != reAccount.OrganizationName ||
                account.OrganizationSlug != reAccount.OrganizationSlug ||
                account.Role != reAccount.Role ||
                account.Status != reAccount.Status ||
                account.UserName != reAccount.UserName
               )
            {
                throw new ApplicationException("Create Account/Get Account did not have the expected result");
            }

            var updateAccountData = new UpdateAccountData() { Description = "Sanity Check Update" };
            var updatedAccount = await manager.UpdateAccountAsync(account.AccountID, updateAccountData);
            reAccount = await manager.GetAccountAsync(account.AccountID);

            if (
                updateAccountData.Description != updatedAccount.Description ||
                updatedAccount.AccountID != reAccount.AccountID ||
                updatedAccount.AccountType != reAccount.AccountType ||
                updatedAccount.Description != reAccount.Description ||
                updatedAccount.Identity != reAccount.Identity ||
                updatedAccount.OrganizationName != reAccount.OrganizationName ||
                updatedAccount.OrganizationSlug != reAccount.OrganizationSlug ||
                updatedAccount.Role != reAccount.Role ||
                updatedAccount.Status != reAccount.Status ||
                updatedAccount.UserName != reAccount.UserName
            )
            {
                throw new ApplicationException("Update Account/Get Account did not have the expected result");
            }

            var accountList = await manager.GetAccountsAsync(1);
            var fAccount = accountList.Results.FirstOrDefault();
            if (
                fAccount == null ||
                string.IsNullOrEmpty(fAccount.AccountID) ||
                string.IsNullOrEmpty(fAccount.AccountType) ||
                string.IsNullOrEmpty(fAccount.Description) ||
                !string.IsNullOrEmpty(fAccount.Identity) ||     // Expect identity to be null because we created a 'User' type account
                string.IsNullOrEmpty(fAccount.OrganizationName) ||
                string.IsNullOrEmpty(fAccount.OrganizationSlug) ||
                string.IsNullOrEmpty(fAccount.Role) ||
                string.IsNullOrEmpty(fAccount.Status) ||
                string.IsNullOrEmpty(fAccount.UserName)
            )
            {
                throw new ApplicationException("Get Account List did not have the expected result");
            }

            // ***** Template tests *****
            Console.WriteLine("🟩 Template Tests");
            var createEmailTemplateData = new CreateTemplateData()
            {
                Slug = GenerateUniqueSlug("sanity-template"),
                Locale = "",
                Name = "Sanity Test Email Template",
                Stage = TemplateStage.Draft,
                Status = TemplateStatus.Active,
                Type = MessageType.Email,
                Subject = "Sanity Test Subject",
                Body = "Sanity Test Body"
            };
            emailTemplate = await manager.CreateTemplateAsync(createEmailTemplateData);
            var reEmailTemplate = await manager.GetTemplateAsync(emailTemplate.TemplateID);

            if (
                emailTemplate.Content.Count != 1 ||
                createEmailTemplateData.Slug != emailTemplate.Slug ||
                createEmailTemplateData.Locale != emailTemplate.Locale ||
                createEmailTemplateData.Name != emailTemplate.Name ||
                createEmailTemplateData.Stage != emailTemplate.Content[0].Stage ||
                createEmailTemplateData.Status != emailTemplate.Status ||
                createEmailTemplateData.Type != emailTemplate.Type ||
                createEmailTemplateData.Subject != emailTemplate.Content[0].Subject ||
                createEmailTemplateData.Body != emailTemplate.Content[0].Body ||
                string.IsNullOrEmpty(emailTemplate.TemplateID) ||
                emailTemplate.Content.Count != reEmailTemplate.Content.Count ||
                reEmailTemplate.Content.Count != 1 ||
                emailTemplate.Content[0].Stage != reEmailTemplate.Content[0].Stage ||
                emailTemplate.Content[0].Subject != reEmailTemplate.Content[0].Subject ||
                emailTemplate.Content[0].Body != reEmailTemplate.Content[0].Body ||
                emailTemplate.Content[0].DateLastModified != reEmailTemplate.Content[0].DateLastModified ||
                emailTemplate.TemplateID != reEmailTemplate.TemplateID ||
                emailTemplate.Slug != reEmailTemplate.Slug ||
                emailTemplate.Locale != reEmailTemplate.Locale ||
                emailTemplate.Name != reEmailTemplate.Name ||
                emailTemplate.Status != reEmailTemplate.Status ||
                emailTemplate.Type != reEmailTemplate.Type
            )
            {
                throw new ApplicationException("Create template/Get template did not have the expected result");
            }

            var updateEmailTemplateData = new UpdateTemplateData() { Name = "Sanity Test Email Template Update", Stage = TemplateStage.Published, Subject = "Sanity Test Subject", Body = "Sanity Test Body" };
            var updatedEmailTemplate = await manager.UpdateTemplateAsync(emailTemplate.TemplateID, updateEmailTemplateData);
            reEmailTemplate = await manager.GetTemplateAsync(emailTemplate.TemplateID);

            if (
                updateEmailTemplateData.Name != updatedEmailTemplate.Name ||
                updatedEmailTemplate.TemplateID != reEmailTemplate.TemplateID ||
                updatedEmailTemplate.Slug != reEmailTemplate.Slug ||
                updatedEmailTemplate.Locale != reEmailTemplate.Locale ||
                updatedEmailTemplate.Status != reEmailTemplate.Status ||
                updatedEmailTemplate.Type != reEmailTemplate.Type ||
                updatedEmailTemplate.Content.Count != 1 ||
                reEmailTemplate.Content.Count != 1 ||
                updatedEmailTemplate.Content[0].Body != reEmailTemplate.Content[0].Body ||
                updatedEmailTemplate.Content[0].Subject != reEmailTemplate.Content[0].Subject ||
                updatedEmailTemplate.Content[0].Stage != reEmailTemplate.Content[0].Stage ||
                updatedEmailTemplate.Content[0].Stage != updateEmailTemplateData.Stage ||
                updatedEmailTemplate.Content[0].DateLastModified != reEmailTemplate.Content[0].DateLastModified
            )
            {
                throw new ApplicationException("Update template/Get template did not have the expected result");
            }

            smsTemplate = await manager.CreateTemplateAsync(new CreateTemplateData()
            {
                Slug = GenerateUniqueSlug("sanity-template"),
                Locale = "",
                Name = "Sanity Test SMS Template",
                Stage = TemplateStage.Published,
                Status = TemplateStatus.Active,
                Type = MessageType.Sms,
                Body = "Sanity Test Body"
            });
            var reSmsTemplate = await manager.GetTemplateAsync(smsTemplate.TemplateID);

            if (reSmsTemplate == null)
            {
                throw new ApplicationException("Create template/Get template (sms) did not have the expected result");
            }

            var templateList = await manager.GetTemplatesAsync(1);
            var fTemplate = templateList.Results.FirstOrDefault();
            if (
                fTemplate == null ||
                string.IsNullOrEmpty(fTemplate.TemplateID)
            )
            {
                 throw new ApplicationException("Get Template List did not have the expected result");
            }


            // ***** Sender tests *****
            Console.WriteLine("🟩 Sender Tests");
            var createEmailSenderData = new CreateSenderData()
            {
                Name = "Sanity Test Email Sender",
                Type = MessageType.Email,
                ServiceProvider = ServiceProvider.EmailIntegrationTest,
                Priority = 1,
                Status = SenderStatus.Active,
                SenderConfiguration = new TestSenderConfiguration()
            };
            emailSender = await manager.CreateSenderAsync(createEmailSenderData);
            var reEmailSender = await manager.GetSenderAsync(emailSender.SenderID);

            if (
                createEmailSenderData.Name != emailSender.Name ||
                createEmailSenderData.Type != emailSender.Type ||
                createEmailSenderData.ServiceProvider != emailSender.ServiceProvider ||
                createEmailSenderData.Priority != emailSender.Priority ||
                createEmailSenderData.Status != emailSender.Status ||
                emailSender.SenderConfiguration == null ||
                string.IsNullOrEmpty(emailSender.SenderID) ||
                emailSender.SenderID != reEmailSender.SenderID ||
                emailSender.Name != reEmailSender.Name ||
                emailSender.ServiceProvider != reEmailSender.ServiceProvider ||
                emailSender.Priority != reEmailSender.Priority ||
                emailSender.Status  != reEmailSender.Status ||
                reEmailSender.ServiceProvider == null
            )
            {
                throw new ApplicationException("Create sender/Get sender did not have the expected result");
            }

            var updateEmailSenderData = new UpdateSenderData() { Name = "Sanity Test Email Sender Update" };
            var updatedSender = await manager.UpdateSenderAsync(emailSender.SenderID, updateEmailSenderData);
            reEmailSender = await manager.GetSenderAsync(emailSender.SenderID);

            if (
                updateEmailSenderData.Name != updatedSender.Name ||
                updatedSender.Name != reEmailSender.Name ||
                updatedSender.SenderID != reEmailSender.SenderID ||
                updatedSender.Type != reEmailSender.Type ||
                updatedSender.ServiceProvider != reEmailSender.ServiceProvider ||
                updatedSender.Priority != reEmailSender.Priority ||
                updatedSender.Status != reEmailSender.Status ||
                updatedSender.SenderConfiguration == null ||
                reEmailSender.SenderConfiguration == null
            )
            {
                throw new ApplicationException("Update organization/Get Organization did not have the expected result");
            }

            smsSender = await manager.CreateSenderAsync(new CreateSenderData() { Name = "Sanity Test SMS Sender", Type = MessageType.Sms, ServiceProvider = ServiceProvider.SmsIntegrationTest, Priority = 1, Status = SenderStatus.Active, SenderConfiguration = new TestSenderConfiguration() });
            var reSmsSender = await manager.GetSenderAsync(smsSender.SenderID);

            var senderList = await manager.GetSendersAsync(1);
            var fSender = senderList.Results.FirstOrDefault();
            if (
                fSender == null ||
                string.IsNullOrEmpty(fSender.SenderID) ||
                string.IsNullOrEmpty(fSender.Name) ||
                string.IsNullOrEmpty(fSender.ServiceProvider) ||
                string.IsNullOrEmpty(fSender.Type) ||
                fSender.Priority == 0 ||
                string.IsNullOrEmpty(fSender.Status) ||
                fSender.SenderConfiguration == null
            )
            {
                throw new ApplicationException("Get Organization List did not have the expected result");
            }

            // ***** Block tests *****
            Console.WriteLine("🟩 Block Tests");
            var createEmailBlockData = new CreateBlockData() { Recipient = "sanity-block-test@example.com", Reason = BlockReasonType.Manual, Description = "Sanity Test Block" };
            emailBlock = await manager.CreateBlockAsync(createEmailBlockData);
            var reBlock = await manager.GetBlockAsync(emailBlock.BlockID);

            if (
                createEmailBlockData.Recipient != emailBlock.Recipient ||
                createEmailBlockData.Reason != emailBlock.Reason ||
                createEmailBlockData.Description != emailBlock.Description ||
                string.IsNullOrEmpty(emailBlock.BlockID) ||
                emailBlock.BlockID != reBlock.BlockID ||
                emailBlock.Description != reBlock.Description ||
                emailBlock.Reason != reBlock.Reason ||
                emailBlock.Recipient != reBlock.Recipient ||
                emailBlock.DateAdded != reBlock.DateAdded
            )
            {
                throw new ApplicationException("Create block/Get block did not have the expected result");
            }

            smsBlock = await manager.CreateBlockAsync(new CreateBlockData() { Recipient = "+18015559999", Reason = BlockReasonType.Manual, Description = "Sanity Test Block" });

            var blockList = await manager.GetBlocksAsync(1);
            var fBlock = blockList.Results.FirstOrDefault();
            if (
                fBlock == null ||
                string.IsNullOrEmpty(fBlock.BlockID) ||
                string.IsNullOrEmpty(fBlock.Description) ||
                string.IsNullOrEmpty(fBlock.Reason) ||
                string.IsNullOrEmpty(fBlock.Recipient)
            )
            {
                throw new ApplicationException("Get Block List did not have the expected result");
            }

            // ***** Manager Send Email tests *****
            Console.WriteLine("🟩 Manager Send Email Tests");
            var createEmailMessageData = new CreateEmailMessage()
            {
                TemplateSlug = emailTemplate.Slug,
                TemplateLocale = emailTemplate.Locale,
                Recipient = new ManageClient.Models.Common.EmailRecipient() { Name = "Sanity Test", Email = "sanity@example.com" },
                MergeValues = new MergeValues() { },
                Metadata = new Dictionary<string, string>() { { "SentBy", "Sanity Test" } },
                SenderID = emailSender.SenderID,
            };
            message1 = await manager.CreateEmailMessageAsync(createEmailMessageData, isTestMessage: false);

            if (string.IsNullOrEmpty(message1.MessageID))
            {
                throw new ApplicationException("Create Email Message did not have the expected result.");
            }

            var reManagerEmailMessage = await manager.GetMessageAsync(message1.MessageID);

            if (
                reManagerEmailMessage == null ||
                reManagerEmailMessage.MessageID != message1.MessageID
               )
            {
                throw new ApplicationException("Create Email Message/Get Message did not have the expected result");
            }

            // ***** Manager Send Sms tests *****
            Console.WriteLine("🟩 Manager Send SMS Tests");
            message2 = await manager.CreateSmsMessageAsync(new CreateSmsMessage()
            {
                TemplateSlug = smsTemplate.Slug,
                TemplateLocale = smsTemplate.Locale,
                Recipient = new ManageClient.Models.Common.SmsRecipient() { Phone = "+18015550011" },
                MergeValues = new MergeValues() {  },
                Metadata = new Dictionary<string, string>() { { "SentBy", "Sanity Test"} },
                SenderID = smsSender.SenderID,
            }, false);

            var reManagerSmsMessage = await manager.GetMessageAsync(message2.MessageID);

            var messageList = await manager.GetMessagesAsync(1);


            // ------------------------------------------

            Console.WriteLine("🟩 Messager Tests");

            var messageAuthConfig = new MessageOidcClientCredentialsConfiguration()
            {
                ClientID = clientID,
                ClientSecret = clientSecret,
                Scope = scope,
                TokenUrl = tokenUrl
            };

            DelegatingHandler messageAuthHandler = enableRequestLogging ?
                new LoggingMessageOidcClientCredentialAuthHeaderHandler(messageAuthConfig, new HttpClient()) :
                new MessageClient.OidcClientCredentialAuthHeaderHandler(messageAuthConfig, new HttpClient());

            var messageHttpClient = new HttpClient(messageAuthHandler)
            {
                BaseAddress = new Uri(baseApiUrl)
            };

            var messager = new NotificationsMessageClient(messageHttpClient, organization.OrganizationID);

            Console.WriteLine("🟩 Message Send Email");
            message3 = await messager.SendEmailAsync(new EmailMessage() {
                TemplateSlug = emailTemplate.Slug,
                TemplateLocale = "sn-TY",	// This message will fall back to the default template
                Recipient = new EmailRecipient() {
                    Name = "Sanity Check",
                    Email = "sanity.check@example.com"
                },
                MergeValues = new MergeValues() {
                    Thing1 = "Sanity Johnson",
                    Thing2 = "$100.00",
                },
                Metadata = new Dictionary<string, string>() { { "sentBy", "Sanity Test" } },
                SenderID = emailSender.SenderID,
            });

            // Reload the message and check that it handled the fallback
            var reMessagerEmailMessage = await manager.GetMessageAsync(message3.MessageID);

            Console.WriteLine("🟩 Messager Send SMS Test");
            message4 = await messager.SendSmsAsync(new SmsMessage() {
                TemplateSlug = smsTemplate.Slug,
                TemplateLocale = smsTemplate.Locale,
                Recipient = new SmsRecipient() {
                    Phone = "+18015550011",
                },
                MergeValues = new MergeValues() {
                    Thing1 = "Sanity Smith",
                    Thing2 = "$100.00",
                },
                Metadata = new Dictionary<string, string>() { { "SentBy", "Sanity Test"} },
                SenderID = smsSender.SenderID,
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"🟥 {ex}");
            throw;
        }
        finally
        {
            Console.WriteLine("🟩 Record Cleanup");
            try { await DoIfNotNull(message1?.MessageID, async (string id) => await manager.DeleteMessageAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting message1", ex); }
            try { await DoIfNotNull(message2?.MessageID, async (string id) => await manager.DeleteMessageAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting message2", ex); }
            try { await DoIfNotNull(message3?.MessageID, async (string id) => await manager.DeleteMessageAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting message3", ex); }
            try { await DoIfNotNull(message4?.MessageID, async (string id) => await manager.DeleteMessageAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting message4", ex); }

            try { await DoIfNotNull(emailBlock?.BlockID, async (string id) => await manager.DeleteBlockAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting emailBlock", ex); }
            try { await DoIfNotNull(smsBlock?.BlockID, async (string id) => await manager.DeleteBlockAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting smsBlock", ex); }
            try { await DoIfNotNull(emailSender?.SenderID, async (string id) => await manager.DeleteSenderAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting emailSender", ex); }
            try { await DoIfNotNull(smsSender?.SenderID, async (string id) => await manager.DeleteSenderAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting smsSender", ex); }
            try { await DoIfNotNull(emailTemplate?.TemplateID, async (string id) => await manager.DeleteTemplateAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting emailTemplate", ex); }
            try { await DoIfNotNull(smsTemplate?.TemplateID, async (string id) => await manager.DeleteTemplateAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting smsTemplate", ex); }
            try { await DoIfNotNull(account?.AccountID, async (string id) => await manager.DeleteAccountAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleteing account", ex); }
            try { await DoIfNotNull(organization?.OrganizationID, async (string id) => await manager.DeleteOrganizationAsync(id)); } catch (Exception ex) { Console.WriteLine("🟥 Error deleting organization", ex); }
        }
    }

    private static async Task DoIfNotNull(string? id, Func<string, Task> thingToDo)
    {
        if (id != null)
        {
            await thingToDo(id);
        }
    }
}

public record MergeValues
{
    public string? Thing1 { get; set; }
    public string? Thing2 { get; set; }
}
