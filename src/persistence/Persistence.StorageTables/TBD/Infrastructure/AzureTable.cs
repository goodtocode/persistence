using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GoodToCode.Infrastructure.Covid19
{
    public class AzureTable<TEntity> where TEntity : TableEntity, new()
    {
        private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=GoodToCodecovid19;AccountKey=l6hLtpU4PfEtwbc5bfCBym01IxhnY8qJDxTRL48tRsEjCR0vrPoswpIMRHpLAfhbCzxU6DhOlq3bDcuIP8sMpA==;EndpointSuffix=core.windows.net";
        private readonly string _tableName = string.Empty;
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _table;
        private readonly string _partitionKey = string.Empty;
        private readonly ILogger _log;

        public AzureTable(string tableName, string partitionKey, ILogger log)
        {
            _log = log;
            _tableName = tableName;
            _storageAccount = CloudStorageAccount.Parse(_connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(_tableName);
            _partitionKey = partitionKey;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var returnData = new List<TEntity>();
            TableContinuationToken token = null;

            do
            {
                var query = new TableQuery<TEntity>() { FilterString = "" };
                var queryResponse = await _table.ExecuteQuerySegmentedAsync<TEntity>(query, token, null, null);
                token = queryResponse.ContinuationToken;
                returnData.AddRange(queryResponse);
            }
            while (token != null);
            return returnData;
        }

        public async Task<List<TEntity>> GetWorldwideAsync()
        {
            TableQuery<TEntity> query = new TableQuery<TEntity>()
                .Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("Country_Region", QueryComparisons.Equal, "Worldwide"),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("AdminRegion1", QueryComparisons.Equal, ""))
                    );
            var returnData = new List<TEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            return returnData;
        }

        public async Task<List<TEntity>> GetByCountryAsync(string country)
        {
            TableQuery<TEntity> query = new TableQuery<TEntity>()
                .Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("Country_Region", QueryComparisons.Equal, country),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("AdminRegion1", QueryComparisons.Equal, ""))
                    );
            var returnData = new List<TEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            return returnData;
        }

        public async Task<List<TEntity>> GetByStateAsync(string state)
        {
            TableQuery<TEntity> query = new TableQuery<TEntity>()
                .Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("AdminRegion1", QueryComparisons.Equal, state),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("AdminRegion2", QueryComparisons.Equal, ""))
                    );
            var returnData = new List<TEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            return returnData;
        }

        public async Task<List<TEntity>> GetByCountyAsync(string state, string county)
        {
            TableQuery<TEntity> query = new TableQuery<TEntity>()
                .Where(
                    TableQuery.CombineFilters(
                            TableQuery.GenerateFilterCondition("AdminRegion1", QueryComparisons.Equal, state),
                            TableOperators.And,
                            TableQuery.GenerateFilterCondition("AdminRegion2", QueryComparisons.Equal, county))
                );
            var returnData = new List<TEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            return returnData;
        }

        public async Task<List<TEntity>> GetByCountyAsync(DateTime latestDate, string state, string county)
        {
            TableQuery<TEntity> query = new TableQuery<TEntity>()
                .Where(
                    TableQuery.CombineFilters(TableQuery.GenerateFilterConditionForDate("Date", QueryComparisons.LessThanOrEqual, latestDate.Date),
                        TableOperators.And,
                        TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("AdminRegion1", QueryComparisons.Equal, state),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("AdminRegion2", QueryComparisons.Equal, county)))
                    );
            var returnData = new List<TEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            return returnData;
        }

        public async Task<TableResult> GetByKeyAsync(string rowKey)
        {
            var retrieveOperation = TableOperation.Retrieve<TEntity>(_partitionKey, rowKey);
            var result = await _table.ExecuteAsync(retrieveOperation);
            return result;
        }

        public async Task<TableResult> InsertOrReplaceAsync(TableEntity entity)
        {
            TableResult result;
            entity.PartitionKey = _partitionKey;
            string currentId = string.Empty;
            try
            {
                var insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
                result = await _table.ExecuteAsync(insertOrMergeOperation);
            }
            catch (Exception ex)
            {
                _log.LogError($"{ex.Message} - Id:{currentId}");
                throw ex;
            }

            return result;
        }

        public async Task<List<HospitalizationEntity>> GetByHospitalizedPatientsDesc(DateTime targetDate, string[] exlcudeCounties)
        {
            var queryFilters = $"AdminRegion1 eq 'California' and (Date ge datetime'{targetDate:yyyy-MM-ddT00:00:00.000Z}' and Date lt datetime'{targetDate.Date.AddDays(1):yyyy-MM-ddT00:00:00.000Z}')";
            TableQuery<HospitalizationEntity> query = new TableQuery<HospitalizationEntity>()
                .Where(queryFilters);
            var returnData = new List<HospitalizationEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                var data = await _table.ExecuteQuerySegmentedAsync(query, continuationToken);
                returnData.AddRange(data);
                continuationToken = data.ContinuationToken;
            } while (continuationToken != null);

            if (exlcudeCounties != null)
            {
                return returnData.Where(x => !exlcudeCounties.Contains(x.AdminRegion2, StringComparer.OrdinalIgnoreCase))
                    .OrderByDescending(x => x.HospitalizedPatients).ToList();
            }
            return returnData.OrderByDescending(x => x.HospitalizedPatients).ToList();
        }
    }
}
