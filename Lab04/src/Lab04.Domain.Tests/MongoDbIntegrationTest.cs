using System;
using Mongo2Go;
using MongoDB.Driver;

namespace Lab04.Domain.Tests
{
    // base class to easily add mongo db to a test class
    public class MongoDbIntegrationTest : IDisposable
    {
        MongoDbRunner runner;
        // protected scope to make the client available to derived classes
        protected IMongoClient mongoClient;

        // xunit creates a new test object for each test
        // this means that we get a whole new database instance for each test
        // full isolation!
        public MongoDbIntegrationTest()
        {
            // creates an in memory mongodb instance
            runner = MongoDbRunner.Start(singleNodeReplSet: false);

            // creates a mongodb client instance to pass to our repository
            // the runner has provided a connection string.
            mongoClient = new MongoClient(runner.ConnectionString);
        }

        // since mongodb runner uses unmanged code, make sure to clean up
        // by calling dispose
        public void Dispose()
        {
            runner.Dispose();
        }
    }
}