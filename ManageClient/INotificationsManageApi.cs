using ManageClient.Models.Account;
using ManageClient.Models.Block;
using ManageClient.Models.Info;
using ManageClient.Models.Message;
using ManageClient.Models.Organization;
using ManageClient.Models.Sender;
using ManageClient.Models.Template;
using Refit;

namespace ManageClient;

[Headers("Content Type: application/json")]
public interface INotificationsManageApi
{
    #region Info
    [Headers("Accept: application/vnd.notification.get-info.v1+json")]
    [Get("/manage/info")]
    public Task<AuthenticatedAccountInfo> GetInfo();

    [Headers("Accept: application/vnd.notification.create-info.v1+json")]
    [Post("/manage/info")]
    public Task RegisterAccount([Body] RegistrationInfo registrationInfo);
    #endregion

    #region Messages
    [Headers("Accept: application/vnd.notification.list-message.v1+json")]
    [Get("/manage/organization/{organizationID}/messages")]
    public Task<MessageList> GetMessages([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage, string organizationID);

    [Headers("Accept: application/vnd.notification.get-message.v1+json")]
    [Get("/manage/organization/{organizationID}/message/{messageID}")]
    public Task<Message> GetMessage(string messageID, string organizationID);

    [Headers("Accept: application/vnd.notification.create-email-message.v1+json")]
    [Post("/manage/organization/{organizationID}/message")]
    public Task<CreateMessageResult> CreateEmailMessage([Body] CreateEmailMessage emailMessage, string organizationID);

    [Headers("Accept: application/vnd.notification.create-test-email-message.v1+json")]
    [Post("/manage/organization/{organizationID}/message")]
    public Task<CreateMessageResult> CreateTestEmailMessage([Body] CreateEmailMessage emailMessage, string organizationID);

    [Headers("Accept: application/vnd.notification.create-sms-message.v1+json")]
    [Post("/manage/organization/{organizationID}/message")]
    public Task<CreateMessageResult> CreateSmsMessage([Body] CreateSmsMessage smsMessage, string organizationID);

    [Headers("Accept: application/vnd.notification.create-test-sms-message.v1+json")]
    [Post("/manage/organization/{organizationID}/message")]
    public Task<CreateMessageResult> CreateTestSmsMessage([Body] CreateSmsMessage smsMessage, string organizationID);

    [Headers("Accept: application/vnd.notification.delete-message.v1+json")]
    [Delete("/manage/organization/{organizationID}/message/{messageID}")]
    public Task DeleteMessage(string messageID, string organizationID);

    #endregion

    #region Organizations
    [Headers("Accept: application/vnd.notification.list-organization.v1+json")]
    [Get("/manage/organizations")]
    public Task<OrganizationList> GetOrganizations([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage);

    [Headers("Accept: application/vnd.notification.get-organization.v1+json")]
    [Get("/manage/organization/{organizationID}")]
    public Task<Organization> GetOrganization(string organizationID);

    [Headers("Accept: application/vnd.notification.create-organization.v1+json")]
    [Post("/manage/organization")]
    public Task<Organization> CreateOrganization([Body] CreateOrganizationData organization);

    [Headers("Accept: application/vnd.notification.update-organization.v1+json")]
    [Patch("/manage/organization/{organizationID}")]
    public Task<Organization> UpdateOrganization(string organizationID, [Body] UpdateOrganizationData organization);

    [Headers("Accept: application/vnd.notification.delete-organization.v1+json")]
    [Delete("/manage/organization/{organizationID}")]
    public Task DeleteOrganization(string organizationID);
    #endregion

    #region Accounts
    [Headers("Accept: application/vnd.notification.list-account.v1+json")]
    [Get("/manage/organization/{organizationID}/accounts")]
    public Task<AccountList> GetAccounts([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage, string organizationID);

    [Headers("Accept: application/vnd.notification.get-account.v1+json")]
    [Get("/manage/organization/{organizationID}/account/{accountID}")]
    public Task<Account> GetAccount(string accountID, string organizationID);

    [Headers("Accept: application/vnd.notification.create-account.v1+json")]
    [Post("/manage/organization/{organizationID}/account")]
    public Task<AccountSecret> CreateAccount([Body] CreateAccountData account, string organizationID);

    [Headers("Accept: application/vnd.notification.update-account.v1+json")]
    [Patch("/manage/organization/{organizationID}/account/{accountID}")]
    public Task<Account> UpdateAccount(string accountID, [Body] UpdateAccountData account, string organizationID);

    [Headers("Accept: application/vnd.notification.delete-account.v1+json")]
    [Delete("/manage/organization/{organizationID}/account/{accountID}")]
    public Task DeleteAccount(string accountID, string organizationID);
    #endregion

    #region Templates
    [Headers("Accept: application/vnd.notification.list-template.v1+json")]
    [Get("/manage/organization/{organizationID}/templates")]
    public Task<TemplateList> GetTemplates([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage, string organizationID);

    [Headers("Accept: application/vnd.notification.get-template.v1+json")]
    [Get("/manage/organization/{organizationID}/template/{templateID}")]
    public Task<Template> GetTemplate(string templateID, string organizationID);

    [Headers("Accept: application/vnd.notification.create-template.v1+json")]
    [Post("/manage/organization/{organizationID}/template")]
    public Task<Template> CreateTemplate([Body] CreateTemplateData template, string organizationID);

    [Headers("Accept: application/vnd.notification.update-template.v1+json")]
    [Patch("/manage/organization/{organizationID}/template/{templateID}")]
    public Task<Template> UpdateTemplate(string templateID, [Body] UpdateTemplateData template, string organizationID);

    [Headers("Accept: application/vnd.notification.delete-template.v1+json")]
    [Delete("/manage/organization/{organizationID}/template/{templateID}")]
    public Task DeleteTemplate(string templateID, string organizationID);
    #endregion

    #region Senders
    [Headers("Accept: application/vnd.notification.list-sender.v1+json")]
    [Get("/manage/organization/{organizationID}/senders")]
    public Task<SenderList> GetSenders([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage, string organizationID);

    [Headers("Accept: application/vnd.notification.get-sender.v1+json")]
    [Get("/manage/organization/{organizationID}/sender/{senderID}")]
    public Task<Sender> GetSender(string senderID, string organizationID);

    [Headers("Accept: application/vnd.notification.create-sender.v1+json")]
    [Post("/manage/organization/{organizationID}/sender")]
    public Task<Sender> CreateSender([Body] CreateSenderData sender, string organizationID);

    [Headers("Accept: application/vnd.notification.update-sender.v1+json")]
    [Patch("/manage/organization/{organizationID}/sender/{senderID}")]
    public Task<Sender> UpdateSender(string senderID, [Body] UpdateSenderData sender, string organizationID);

    [Headers("Accept: application/vnd.notification.delete-sender.v1+json")]
    [Delete("/manage/organization/{organizationID}/sender/{senderID}")]
    public Task DeleteSender(string senderID, string organizationID);
    #endregion

    #region Blocks
    [Headers("Accept: application/vnd.notification.list-block.v1+json")]
    [Get("/manage/organization/{organizationID}/blocks")]
    public Task<BlockList> GetBlocks([AliasAs("pagesize")] int pageSize, [AliasAs("nextpage")] string? nextPage, string organizationID);

    [Headers("Accept: application/vnd.notification.get-block.v1+json")]
    [Get("/manage/organization/{organizationID}/block/{blockID}")]
    public Task<Block> GetBlock(string blockID, string organizationID);

    [Headers("Accept: application/vnd.notification.create-block.v1+json")]
    [Post("/manage/organization/{organizationID}/block")]
    public Task<Block> CreateBlock([Body] CreateBlockData block, string organizationID);

    [Headers("Accept: application/vnd.notification.delete-block.v1+json")]
    [Delete("/manage/organization/{organizationID}/block/{blockID}")]
    public Task DeleteBlock(string blockID, string organizationID);
    #endregion
}
