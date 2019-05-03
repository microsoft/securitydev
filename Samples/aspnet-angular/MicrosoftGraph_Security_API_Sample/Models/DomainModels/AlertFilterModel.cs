// -----------------------------------------------------------------------
// <copyright file="AlertFilterModel.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MicrosoftGraph_Security_API_Sample.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MicrosoftGraph_Security_API_Sample.Models
{
    public interface IFilterValueCreator
    {
        string Create(string value);
    }

    public class AlertFilterModel : IEnumerable
    {
        // List of all filter properties, that can have "All" value (are presented as Dropdowm)
        private static List<string> dropDownFitlerProperties = new List<string> { "alert:severity", "alert:category", "alert:status", "vendor:provider", "alert:feedback" };

        private static Dictionary<string, AlertFilterPropertyDescription> propertyDescriptions = new Dictionary<string, AlertFilterPropertyDescription>
        {
            // Alert
            { "alert:id", new AlertFilterPropertyDescription("AlertId", AlertFilterOperator.Equals, new NumberFilterValueCreator()) },
            { "alert:category", new AlertFilterPropertyDescription("Category", AlertFilterOperator.Equals) },
            { "alert:severity", new AlertFilterPropertyDescription("Severity", AlertFilterOperator.Equals) },
            { "alert:status", new AlertFilterPropertyDescription("Status", AlertFilterOperator.Equals) },
            { "alert:title", new AlertFilterPropertyDescription("Title", AlertFilterOperator.Equals) },
            { "alert:assignedto", new AlertFilterPropertyDescription("AssignedTo", AlertFilterOperator.Equals) },
            { "alert:feedback", new AlertFilterPropertyDescription("feedback", AlertFilterOperator.Equals) },
            { "alert:createddatetime", new AlertFilterPropertyDescription("createdDateTime", AlertFilterOperator.Equals, new DateTimeFilterValueCreator()) },
            { "alert:startdatetime", new AlertFilterPropertyDescription("createdDateTime", AlertFilterOperator.GreaterThanOrEquals, new DateTimeFilterValueCreator()) },
            { "alert:enddatetime", new AlertFilterPropertyDescription("createdDateTime", AlertFilterOperator.LessThanOrEquals, new DateTimeFilterValueCreator()) },

            // Vendor
            { "vendor:vendor", new AlertFilterPropertyDescription("vendorInformation/vendor", AlertFilterOperator.Equals) },
            { "vendor:provider", new AlertFilterPropertyDescription("vendorInformation/provider", AlertFilterOperator.Equals) },
            { "vendor:subprovider", new AlertFilterPropertyDescription("vendorInformation/subProvider", AlertFilterOperator.Equals) },
            { "vendor:version", new AlertFilterPropertyDescription("vendorInformation/providerVersion", AlertFilterOperator.Equals) },

            // User states
            { "user:aaduserid", new AlertFilterPropertyDescription("userStates/any(a:a/aadUserId)", AlertFilterOperator.Equals) },
            { "user:accountname", new AlertFilterPropertyDescription("userStates/any(a:a/accountName)", AlertFilterOperator.Equals) },
            { "user:domainname", new AlertFilterPropertyDescription("userStates/any(a:a/domainName)", AlertFilterOperator.Equals) },
            { "user:emailrole", new AlertFilterPropertyDescription("userStates/any(a:a/emailRole)", AlertFilterOperator.Equals) },
            { "user:isvpn", new AlertFilterPropertyDescription("userStates/any(a:a/isVpn)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },
            { "user:logondatetime", new AlertFilterPropertyDescription("userStates/any(a:a/logonDateTime)", AlertFilterOperator.Equals, new DateTimeFilterValueCreator()) },
            { "user:logonid", new AlertFilterPropertyDescription("userStates/any(a:a/logonId)", AlertFilterOperator.Equals) },
            { "user:logonip", new AlertFilterPropertyDescription("userStates/any(a:a/logonIp)", AlertFilterOperator.Equals) },
            { "user:logonlocation", new AlertFilterPropertyDescription("userStates/any(a:a/logonLocation)", AlertFilterOperator.Equals) },
            { "user:logontype", new AlertFilterPropertyDescription("userStates/any(a:a/logonType)", AlertFilterOperator.Equals) },
            { "user:onpremisessecurityidentifier", new AlertFilterPropertyDescription("userStates/any(a:a/onPremisesSecurityIdentifier)", AlertFilterOperator.Equals) },
            { "user:riskscore", new AlertFilterPropertyDescription("userStates/any(a:a/riskScore)", AlertFilterOperator.Equals) },
            { "user:accounttype", new AlertFilterPropertyDescription("userStates/any(a:a/userAccountType)", AlertFilterOperator.Equals) },
            { "user:upn", new AlertFilterPropertyDescription("userStates/any(a:a/userPrincipalName)", AlertFilterOperator.Equals) },

            // Host states
            { "host:fqdn", new AlertFilterPropertyDescription("hostStates/any(a:a/fqdn)", AlertFilterOperator.Equals) },
            { "host:isazureadjoined", new AlertFilterPropertyDescription("hostStates/any(a:a/isAzureAdJoined)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },
            { "host:isazureadregistered", new AlertFilterPropertyDescription("hostStates/any(a:a/isAzureAdRegistered)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },
            { "host:ishybridazuredomainjoined", new AlertFilterPropertyDescription("hostStates/any(a:a/isHybridAzureDomainJoined)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },
            { "host:netbiosname", new AlertFilterPropertyDescription("hostStates/any(a:a/netBiosName)", AlertFilterOperator.Equals) },
            { "host:os", new AlertFilterPropertyDescription("hostStates/any(a:a/os)", AlertFilterOperator.Equals) },
            { "host:privateipaddress", new AlertFilterPropertyDescription("hostStates/any(a:a/privateIpAddress)", AlertFilterOperator.Equals) },
            { "host:publicipaddress", new AlertFilterPropertyDescription("hostStates/any(a:a/publicIpAddress)", AlertFilterOperator.Equals) },
            { "host:riskscore", new AlertFilterPropertyDescription("hostStates/any(a:a/riskScore)", AlertFilterOperator.Equals) },

            // Triggers
            { "trigger:name", new AlertFilterPropertyDescription("triggers/any(a:a/name)", AlertFilterOperator.Equals) },
            { "trigger:value", new AlertFilterPropertyDescription("triggers/any(a:a/value)", AlertFilterOperator.Equals) },
            { "trigger:type", new AlertFilterPropertyDescription("triggers/any(a:a/type)", AlertFilterOperator.Equals) },

            // Network connections
            { "netconn:applicationname", new AlertFilterPropertyDescription("networkConnections/any(a:a/applicationName)", AlertFilterOperator.Equals) },
            { "netconn:destinationaddress", new AlertFilterPropertyDescription("networkConnections/any(a:a/destinationAddress)", AlertFilterOperator.Equals) },
            { "netconn:destinationdomain", new AlertFilterPropertyDescription("networkConnections/any(a:a/destinationDomain)", AlertFilterOperator.Equals) },
            { "netconn:destinationport", new AlertFilterPropertyDescription("networkConnections/any(a:a/destinationPort)", AlertFilterOperator.Equals) },
            { "netconn:destinationurl", new AlertFilterPropertyDescription("networkConnections/any(a:a/destinationUrl)", AlertFilterOperator.Equals) },
            { "netconn:direction", new AlertFilterPropertyDescription("networkConnections/any(a:a/direction)", AlertFilterOperator.Equals) },
            { "netconn:domainregistereddatetime", new AlertFilterPropertyDescription("networkConnections/any(a:a/domainRegisteredDateTime)", AlertFilterOperator.Equals, new DateTimeFilterValueCreator()) },
            { "netconn:localdnsname", new AlertFilterPropertyDescription("networkConnections/any(a:a/localDnsName)", AlertFilterOperator.Equals) },
            { "netconn:natdestinationaddress", new AlertFilterPropertyDescription("networkConnections/any(a:a/natDestinationAddress)", AlertFilterOperator.Equals) },
            { "netconn:natdestinationport", new AlertFilterPropertyDescription("networkConnections/any(a:a/natDestinationPort)", AlertFilterOperator.Equals) },
            { "netconn:natsourceaddress", new AlertFilterPropertyDescription("networkConnections/any(a:a/natSourceAddress)", AlertFilterOperator.Equals) },
            { "netconn:natsourceport", new AlertFilterPropertyDescription("networkConnections/any(a:a/natSourcePort)", AlertFilterOperator.Equals) },
            { "netconn:protocol", new AlertFilterPropertyDescription("networkConnections/any(a:a/protocol)", AlertFilterOperator.Equals) },
            { "netconn:riskscore", new AlertFilterPropertyDescription("networkConnections/any(a:a/riskScore)", AlertFilterOperator.Equals) },
            { "netconn:sourceaddress", new AlertFilterPropertyDescription("networkConnections/any(a:a/sourceAddress)", AlertFilterOperator.Equals) },
            { "netconn:sourceport", new AlertFilterPropertyDescription("networkConnections/any(a:a/sourceport)", AlertFilterOperator.Equals) },
            { "netconn:status", new AlertFilterPropertyDescription("networkConnections/any(a:a/status)", AlertFilterOperator.Equals) },
            { "netconn:urlparameters", new AlertFilterPropertyDescription("networkConnections/any(a:a/urlParameters)", AlertFilterOperator.Equals) },

            // Files
            { "file:name", new AlertFilterPropertyDescription("fileStates/any(a:a/name)", AlertFilterOperator.Equals) },
            { "file:path", new AlertFilterPropertyDescription("fileStates/any(a:a/path)", AlertFilterOperator.Equals) },
            { "file:riskscore", new AlertFilterPropertyDescription("fileStates/any(a:a/riskScore)", AlertFilterOperator.Equals) },

            // Processes
            { "process:accountname", new AlertFilterPropertyDescription("processes/any(a:a/accountName)", AlertFilterOperator.Equals) },
            { "process:commandline", new AlertFilterPropertyDescription("processes/any(a:a/commandLine)", AlertFilterOperator.Equals) },
            { "process:createddatetime", new AlertFilterPropertyDescription("processes/any(a:a/createdDateTime)", AlertFilterOperator.Equals, new DateTimeFilterValueCreator()) },
            { "process:integritylevel", new AlertFilterPropertyDescription("processes/any(a:a/integrityLevel)", AlertFilterOperator.Equals) },
            { "process:iselevated", new AlertFilterPropertyDescription("processes/any(a:a/isElevated)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },
            { "process:name", new AlertFilterPropertyDescription("processes/any(a:a/name)", AlertFilterOperator.Equals) },
            { "process:parentprocesscreateddatetime", new AlertFilterPropertyDescription("processes/any(a:a/parentProcessCreatedDateTime)", AlertFilterOperator.Equals, new DateTimeFilterValueCreator()) },
            { "process:parentprocessid", new AlertFilterPropertyDescription("processes/any(a:a/parentProcessId)", AlertFilterOperator.Equals, new NumberFilterValueCreator()) },
            { "process:parentprocessname", new AlertFilterPropertyDescription("processes/any(a:a/parentProcessName)", AlertFilterOperator.Equals) },
            { "process:path", new AlertFilterPropertyDescription("processes/any(a:a/path)", AlertFilterOperator.Equals) },
            { "process:processid", new AlertFilterPropertyDescription("processes/any(a:a/processId)", AlertFilterOperator.Equals, new NumberFilterValueCreator()) },

            // Key Updates
            { "regkeyupdate:hive", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/hive)", AlertFilterOperator.Equals) },
            { "regkeyupdate:valuetype", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/valueType)", AlertFilterOperator.Equals) },
            { "regkeyupdate:operation", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/operation)", AlertFilterOperator.Equals) },
            { "regkeyupdate:oldkey", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/oldKey)", AlertFilterOperator.Equals) },
            { "regkeyupdate:oldvaluename", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/oldValueName)", AlertFilterOperator.Equals) },
            { "regkeyupdate:oldvaluedata", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/oldValueData)", AlertFilterOperator.Equals) },
            { "regkeyupdate:key", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/key)", AlertFilterOperator.Equals) },
            { "regkeyupdate:valuename", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/valueName)", AlertFilterOperator.Equals) },
            { "regkeyupdate:valuedata", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/valueData)", AlertFilterOperator.Equals) },
            { "regkeyupdate:processid", new AlertFilterPropertyDescription("registryKeyStates/any(a:a/processId)", AlertFilterOperator.Equals, new NumberFilterValueCreator()) },

            // Malwares
            { "malware:name", new AlertFilterPropertyDescription("malwareStates/any(a:a/name)", AlertFilterOperator.Equals) },
            { "malware:category", new AlertFilterPropertyDescription("malwareStates/any(a:a/category)", AlertFilterOperator.Equals) },
            { "malware:family", new AlertFilterPropertyDescription("malwareStates/any(a:a/family)", AlertFilterOperator.Equals) },
            { "malware:severity", new AlertFilterPropertyDescription("malwareStates/any(a:a/severity)", AlertFilterOperator.Equals) },
            { "malware:wasrunning", new AlertFilterPropertyDescription("malwareStates/any(a:a/wasRunning)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },

            // Tags
            { "tag:title", new AlertFilterPropertyDescription("tags/any(a:a)", AlertFilterOperator.Equals) },

            // Vulnerability States
            { "vulnerability:cve", new AlertFilterPropertyDescription("vulnerabilityStates/any(a:a/cve)", AlertFilterOperator.Equals) },
            { "vulnerability:severity", new AlertFilterPropertyDescription("vulnerabilityStates/any(a:a/severity)", AlertFilterOperator.Equals) },
            { "vulnerability:wasrunning", new AlertFilterPropertyDescription("vulnerabilityStates/any(a:a/wasRunning)", AlertFilterOperator.Equals, new BooleanFilterValueCreator()) },

            // Cloud App States
            { "cloudapp:destinationservicename", new AlertFilterPropertyDescription("cloudAppStates/any(a:a/destinationServiceName)", AlertFilterOperator.Equals) },
            { "cloudapp:destinationserviceip", new AlertFilterPropertyDescription("cloudAppStates/any(a:a/destinationServiceIp)", AlertFilterOperator.Equals) },
            { "cloudapp:riskscore", new AlertFilterPropertyDescription("cloudAppStates/any(a:a/riskScore)", AlertFilterOperator.Equals) },

            // Detection Ids
            { "detection:id", new AlertFilterPropertyDescription("detectionIds/any(a:a)", AlertFilterOperator.Equals) },
        };

        private Dictionary<string, List<AlertFilterProperty>> filters;

        public AlertFilterModel()
        {
            this.Top = 1;
            this.filters = new Dictionary<string, List<AlertFilterProperty>>();
        }

        public AlertFilterModel(int top)
        {
            this.Top = top;
            this.filters = new Dictionary<string, List<AlertFilterProperty>>();
        }

        public AlertFilterModel(AlertFilterViewModel viewModel)
        {
            if (viewModel != null)
            {
                this.Top = viewModel.Top ?? 1;
                this.filters = new Dictionary<string, List<AlertFilterProperty>>();

                if (viewModel.Filters != null && viewModel.Filters.Count > 0)
                {
                    foreach (KeyValuePair<string, IEnumerable<string>> property in viewModel.Filters)
                    {
                        var isDropDownProperty = dropDownFitlerProperties.Exists(prop => prop.Equals(property.Key, StringComparison.InvariantCultureIgnoreCase));
                        if (!isDropDownProperty)
                        {
                            if (!propertyDescriptions.ContainsKey(property.Key.ToLower()))
                            {
                                throw new Exception($"PropertyDescriptions don't contain specified '{property.Key.ToLower()}' key.");
                            }

                            var propertyDescription = propertyDescriptions[property.Key.ToLower()];
                            if (!this.filters.ContainsKey(propertyDescription.PropertyName))
                            {
                                this.filters.Add(propertyDescription.PropertyName, new List<AlertFilterProperty>());
                            }

                            this.filters[propertyDescription.PropertyName].Add(new AlertFilterProperty(propertyDescription, property.Value.FirstOrDefault()));
                        }
                        else
                        {
                            if (!propertyDescriptions.ContainsKey(property.Key.ToLower()))
                            {
                                throw new Exception($"PropertyDescriptions don't contain specified '{property.Key.ToLower()}' key.");
                            }

                            var propertyDescription = propertyDescriptions[property.Key.ToLower()];
                            if (!this.filters.ContainsKey(propertyDescription.PropertyName))
                            {
                                this.filters.Add(propertyDescription.PropertyName, new List<AlertFilterProperty>());
                            }

                            foreach (var val in property.Value)
                            {
                                this.filters[propertyDescription.PropertyName].Add(new AlertFilterProperty(propertyDescription, val));
                            }
                        }
                    }
                }
            }
        }

        public int Top { get; set; }

        public int Count
        {
            get { return this.filters?.Count ?? 0; }
        }

        public static bool HasPropertyDescription(string key)
        {
            return propertyDescriptions.ContainsKey(key.ToLower());
        }

        public void Add(string key, List<AlertFilterProperty> value)
        {
            if (this.filters == null)
            {
                this.filters = new Dictionary<string, List<AlertFilterProperty>>();
            }
            this.filters.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return this.filters.ContainsKey(key);
        }

        public List<AlertFilterProperty> GetPropertyFilters(string key)
        {
            return this.filters.ContainsKey(key) && this.filters[key] != null ? this.filters[key] : new List<AlertFilterProperty>();
        }

        public bool HasPropertyFilter(string key)
        {
            var filters = this.GetPropertyFilters(key);
            return filters.FirstOrDefault() != null;
        }

        public AlertFilterProperty GetFirstPropertyFilter(string key)
        {
            var filters = this.GetPropertyFilters(key);
            return filters.FirstOrDefault();
        }

        public IEnumerator GetEnumerator()
        {
            return this.filters.GetEnumerator();
        }
    }

    public class AlertFilterPropertyDescription
    {
        public AlertFilterPropertyDescription(string propertyName, AlertFilterOperator actionOperator, IFilterValueCreator valueCreator = null)
        {
            this.PropertyName = propertyName;
            this.Operator = actionOperator;
            this.ValueCreator = valueCreator ?? new DefaultFilterValueCreator();
        }

        public string PropertyName { get; set; }

        public AlertFilterOperator Operator { get; set; }

        public IFilterValueCreator ValueCreator { get; set; }
    }

    public class AlertFilterProperty
    {
        private string value;

        // To learn more: https://developer.microsoft.com/en-us/graph/docs/concepts/query_parameters#filter-parameter
        public AlertFilterProperty(AlertFilterPropertyDescription propertyDescription, string value)
        {
            this.PropertyDescription = propertyDescription;
            this.Value = value;
        }

        public AlertFilterPropertyDescription PropertyDescription { get; set; }

        public string Value
        {
            get { return this.value; }
            set { this.value = this.PropertyDescription.ValueCreator.Create(value); }
        }
    }

    public class AlertFilterOperator
    {
        private AlertFilterOperator(string value)
        {
            this.Value = value;
        }

        public static new AlertFilterOperator Equals
        {
            get { return new AlertFilterOperator("eq"); }
        }

        public static AlertFilterOperator NotEquals
        {
            get { return new AlertFilterOperator("ne"); }
        }

        public static AlertFilterOperator GreaterThan
        {
            get { return new AlertFilterOperator("gt"); }
        }

        public static AlertFilterOperator GreaterThanOrEquals
        {
            get { return new AlertFilterOperator("ge"); }
        }

        public static AlertFilterOperator LessThan
        {
            get { return new AlertFilterOperator("lt"); }
        }

        public static AlertFilterOperator LessThanOrEquals
        {
            get { return new AlertFilterOperator("le"); }
        }

        public static AlertFilterOperator And
        {
            get { return new AlertFilterOperator("and"); }
        }

        public static AlertFilterOperator Or
        {
            get
            {
                return new AlertFilterOperator("or");
            }
        }

        public static AlertFilterOperator Not
        {
            get
            {
                return new AlertFilterOperator("not");
            }
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }

    public class DefaultFilterValueCreator : IFilterValueCreator
    {
        public string Create(string value)
        {
            return $"'{value}'";
        }
    }

    public class DateTimeFilterValueCreator : IFilterValueCreator
    {
        public string Create(string value)
        {
            return $"{DateTime.Parse(value).ToString("yyyy-MM-ddTHH:mm:ssZ")}";
        }
    }

    public class BooleanFilterValueCreator : IFilterValueCreator
    {
        public string Create(string value)
        {
            return $"{value.ToLower()}";
        }
    }

    public class NumberFilterValueCreator : IFilterValueCreator
    {
        public string Create(string value)
        {
            return $"{value}";
        }
    }
}