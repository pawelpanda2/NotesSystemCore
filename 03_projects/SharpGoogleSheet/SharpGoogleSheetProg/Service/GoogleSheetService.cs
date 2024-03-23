﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using SharpGoogleSheetProg.AAPublic;

namespace SharpGoogleSheetProg.Service
{
    public class GoogleSheetService : IGoogleSheetService
    {
        private SheetsService sheetsService;
        private SheetWorker worker;
        private string applicationName;
        private string user;
        private List<string> scopes;
        private string clientId;
        private string clientSecret;
        private bool sheetInit;
        private Dictionary<string, object> settings;

        public GoogleSheetService()
        {
        }

        public GoogleSheetService(
            Dictionary<string, object> settingDict)
        {
            ReWriteSettings(settingDict);
        }

        public SheetWorker Worker
        {
            get
            {
                if (!sheetInit)
                {
                    SheetInit();
                    sheetInit = true;
                }
                return worker;
            }
        }

        public void OverrideSettings(Dictionary<string, object> settingDict)
        {
            this.settings = new Dictionary<string, object>();
            ReWriteSettings(settingDict);
        }

        private void ReWriteSettings(Dictionary<string, object> inputDict)
        {
            this.settings = new Dictionary<string, object>();
            TryAdd(inputDict, settings, "googleClientId");
            TryAdd(inputDict, settings, "googleClientSecret");
            TryAdd(inputDict, settings, "applicationName");
            TryAdd(inputDict, settings, "scopes");
        }

        private void ApplySettings()
        {
            this.clientId = settings["googleClientId"].ToString();
            this.clientSecret = settings["googleClientSecret"].ToString();
            scopes = new List<string>();
            scopes.Add(SheetsService.ScopeConstants.Spreadsheets);
            applicationName = "Gamestatistics";
            user = "gamestatistics";
        }

        private void SheetInit()
        {
            ApplySettings();

            var secrets = new ClientSecrets();
            secrets.ClientId = clientId;
            secrets.ClientSecret = clientSecret;

            var credentialAuthorization = GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets,
                scopes,
                user,
                CancellationToken.None);

            sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentialAuthorization.Result,
                ApplicationName = applicationName,
            });

            worker = new SheetWorker(this, sheetsService);
        }

        private bool TryAdd(
            Dictionary<string, object> inputDict,
            Dictionary<string, object> outputDict,
            string keyName)
        {
            var success1 = inputDict.TryGetValue(keyName, out var valueObj);
            var success2 = false;
            if (success1)
            {
                success2 = outputDict.TryAdd(keyName, valueObj);
            }

            if (success2)
            {
                return true;
            }

            return false;
        }

        //private void InitFields()
        //{
        //    if (scopes == null) { scopes = new List<string>(); };
        //}
    }
}
