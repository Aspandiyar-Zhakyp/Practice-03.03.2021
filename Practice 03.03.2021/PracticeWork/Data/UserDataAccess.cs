using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace PracticeWork.Data
{
  public class UserDataAccess : DbDataAccess
  {
    public bool UserVerification(string userLogin)  // Проверка наличие пользователя в базе
    {
      string script = $"select login from Users;";
      var command = factory.CreateCommand();
      command.CommandText = script;
      command.Connection = connection;

      using (var dataReader = command.ExecuteReader())
      {
        while (dataReader.Read())
        {
          if (userLogin == dataReader["login"].ToString())
          {
            return true;
          };
        }
        return false;
      }
    }
    public bool UserAuthentication(string login,string password)
    {
      string selectScript = $"select * from Users;";
      var command = factory.CreateCommand();
      command.CommandText = selectScript;
      command.Connection = connection;

      var dataReader = command.ExecuteReader();

      while (dataReader.Read())
      {
        if (login == dataReader["login"].ToString() && password == dataReader["password"].ToString())
        {
          return true;
        }
      }
      return false;

    }
    public void UserRegistration(string login,string password)
    {
      string script = $"insert into Users(login,password) values(@Login,@Password);";

      using (var transaction = connection.BeginTransaction())
      using (var command = factory.CreateCommand())
      {
        try
        {
          command.Connection = connection;
          command.CommandText = script;

          command.Transaction = transaction;
          var loginParameter = factory.CreateParameter();
          loginParameter.DbType = System.Data.DbType.String;
          loginParameter.ParameterName = "Login";
          loginParameter.Value = login;

          command.Parameters.Add(loginParameter);

          var passwordParameter = factory.CreateParameter();
          passwordParameter.DbType = System.Data.DbType.String;
          passwordParameter.ParameterName = "Password";
          passwordParameter.Value = password;

          command.Parameters.Add(passwordParameter);
          command.ExecuteNonQuery();
          transaction.Commit();
        }
        catch (DbException ex)
        {
          Console.WriteLine(ex.Message);
          transaction.Rollback();
        }
      }

    }



  }
}
