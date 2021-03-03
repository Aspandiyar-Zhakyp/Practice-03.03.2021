using PracticeWork.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace PracticeWork.Data
{
  public class DbDataAccess:IDisposable
  {
    protected readonly DbProviderFactory factory;
    protected readonly DbConnection connection;
    public DbDataAccess()
    {
      factory = DbProviderFactories.GetFactory("ConnectedLevelProvider");

      connection = factory.CreateConnection();
      connection.ConnectionString = DataAccessConfiguration.Configuration["DataAccessConnectionString"];
      connection.Open();
    }

    public void Dispose()
    {
      connection.Dispose();
    }

  }
}
