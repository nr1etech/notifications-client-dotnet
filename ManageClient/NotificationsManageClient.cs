using System.Text.Json;
using System.Text.Json.Serialization;
using ManageClient.Models.Account;
using ManageClient.Models.Block;
using ManageClient.Models.Info;
using ManageClient.Models.Message;
using ManageClient.Models.Organization;
using ManageClient.Models.Sender;
using ManageClient.Models.Template;
using Refit;

namespace ManageClient;

public class NotificationsManageClient
{
    public string OrganizationID { get; set; }

    private readonly INotificationsManageApi notificationsManageApi;

    public NotificationsManageClient(HttpClient httpClient)
    {
        this.notificationsManageApi = RestService.For<INotificationsManageApi>(httpClient, new RefitSettings()
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }
            ),

        });
    }

    #region Info
    public async Task<AuthenticatedAccountInfo> GetInfoAsync()
    {
        return await this.notificationsManageApi.GetInfo().ConfigureAwait(false);
    }

    public async Task RegisterAccountAsync(RegistrationInfo registrationInfo)
    {
        await this.notificationsManageApi.RegisterAccount(registrationInfo).ConfigureAwait(false);
    }
    #endregion

    #region Messages

    public async Task<MessageList> GetMessagesAsync(int pageSize, string? nextPageID = null, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetMessages(pageSize, nextPageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Message> GetMessageAsync(string messageID, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetMessage(messageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<CreateMessageResult> CreateEmailMessageAsync(CreateEmailMessage emailMessage, bool isTestMessage = false, string? organizationID = null)
    {
        if (isTestMessage)
        {
            return await this.notificationsManageApi.CreateTestEmailMessage(emailMessage, organizationID ?? this.OrganizationID).ConfigureAwait(false);
        }
        else
        {
            return await this.notificationsManageApi.CreateEmailMessage(emailMessage, organizationID ?? this.OrganizationID).ConfigureAwait(false);
        }
    }

    public async Task<CreateMessageResult> CreateSmsMessageAsync(CreateSmsMessage smsMessage, bool isTestMessage = false, string? organizationID = null)
    {
        if (isTestMessage)
        {
            return await this.notificationsManageApi.CreateTestSmsMessage(smsMessage, organizationID ?? this.OrganizationID).ConfigureAwait(false);
        }
        else
        {
            return await this.notificationsManageApi.CreateSmsMessage(smsMessage, organizationID ?? this.OrganizationID).ConfigureAwait(false);
        }
    }

    public async Task DeleteMessageAsync(string messageID, string? organizationID = null)
    {
        await this.notificationsManageApi.DeleteMessage(messageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }
    #endregion

    #region Organizations

    public async Task<OrganizationList> GetOrganizationsAsync(int pageSize, string? nextPageID = null)
    {
        return await this.notificationsManageApi.GetOrganizations(pageSize, nextPageID).ConfigureAwait(false);
    }

    public async Task<Organization> GetOrganizationAsync(string organizationID)
    {
        return await this.notificationsManageApi.GetOrganization(organizationID).ConfigureAwait(false);
    }

    public async Task<Organization> CreateOrganizationAsync(CreateOrganizationData organization)
    {
        return await this.notificationsManageApi.CreateOrganization(organization).ConfigureAwait(false);
    }

    public async Task<Organization> UpdateOrganizationAsync(string organizationID, UpdateOrganizationData organization)
    {
        return await this.notificationsManageApi.UpdateOrganization(organizationID, organization).ConfigureAwait(false);
    }

    public async Task DeleteOrganizationAsync(string organizationID)
    {
        await this.notificationsManageApi.DeleteOrganization(organizationID).ConfigureAwait(false);
    }
    #endregion

    #region Accounts

    public async Task<AccountList> GetAccountsAsync(int pageSize, string? nextPageID = null, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetAccounts(pageSize, nextPageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Account> GetAccountAsync(string accountID, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetAccount(accountID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<AccountSecret> CreateAccountAsync(CreateAccountData account, string? organizationID = null)
    {
        return await this.notificationsManageApi.CreateAccount(account, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Account> UpdateAccountAsync(string accountID, UpdateAccountData account, string? organizationID = null)
    {
        return await this.notificationsManageApi.UpdateAccount(accountID, account, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task DeleteAccountAsync(string accountID, string? organizationID = null)
    {
        await this.notificationsManageApi.DeleteAccount(accountID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }
    #endregion

    #region Templates

    public async Task<TemplateList> GetTemplatesAsync(int pageSize, string? nextPageID = null, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetTemplates(pageSize, nextPageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Template> GetTemplateAsync(string templateID, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetTemplate(templateID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Template> CreateTemplateAsync(CreateTemplateData template, string? organizationID = null)
    {
        return await this.notificationsManageApi.CreateTemplate(template, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Template> UpdateTemplateAsync(string templateID, UpdateTemplateData template, string? organizationID = null)
    {
        return await this.notificationsManageApi.UpdateTemplate(templateID, template, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task DeleteTemplateAsync(string templateID, string? organizationID = null)
    {
        await this.notificationsManageApi.DeleteTemplate(templateID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }
    #endregion

    #region Senders

    public async Task<SenderList> GetSendersAsync(int pageSize, string? nextPageID = null, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetSenders(pageSize, nextPageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Sender> GetSenderAsync(string senderID, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetSender(senderID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Sender> CreateSenderAsync(CreateSenderData sender, string? organizationID = null)
    {
        return await this.notificationsManageApi.CreateSender(sender, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Sender> UpdateSenderAsync(string senderID, UpdateSenderData sender, string? organizationID = null)
    {
        return await this.notificationsManageApi.UpdateSender(senderID, sender, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task DeleteSenderAsync(string senderID, string? organizationID = null)
    {
        await this.notificationsManageApi.DeleteSender(senderID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }
    #endregion

    #region Blocks

    public async Task<BlockList> GetBlocksAsync(int pageSize, string? nextPageID = null, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetBlocks(pageSize, nextPageID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Block> GetBlockAsync(string blockID, string? organizationID = null)
    {
        return await this.notificationsManageApi.GetBlock(blockID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task<Block> CreateBlockAsync(CreateBlockData block, string? organizationID = null)
    {
        return await this.notificationsManageApi.CreateBlock(block, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    public async Task DeleteBlockAsync(string blockID, string? organizationID = null)
    {
        await this.notificationsManageApi.DeleteBlock(blockID, organizationID ?? this.OrganizationID).ConfigureAwait(false);
    }

    #endregion
}

