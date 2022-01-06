using Microsoft.Data.Sqlite;



static void Open(string oldPassword)
{
    string baseConnectionString = "Data Source=local.db";
    var connectionString = new SqliteConnectionStringBuilder(baseConnectionString)
    {
        Mode = SqliteOpenMode.ReadWriteCreate,
        Password = oldPassword
    }.ToString();

    using SqliteConnection? connection = new SqliteConnection(connectionString);
    connection.Open();
    Console.WriteLine($"Open成功");
}


static int UpdatePassword(string oldPassword, string newPassword)
{
    string baseConnectionString = "Data Source=local.db";
    var connectionString = new SqliteConnectionStringBuilder(baseConnectionString)
    {
        Mode = SqliteOpenMode.ReadWriteCreate,
        Password = oldPassword
    }.ToString();

    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT quote($newPassword);";
            command.Parameters.AddWithValue("$newPassword", newPassword);
            var quotedNewPassword = command.ExecuteScalar() as string;

            command.CommandText = "PRAGMA rekey = " + quotedNewPassword;
            command.Parameters.Clear();
            int x = command.ExecuteNonQuery();
            return x;
        }
    }
}

string oldPassword = "123qwe";
string newPassword = "abcd";

//string oldPassword = "abcd";
//string newPassword = "123qwe";

Open(oldPassword);


//UpdatePassword(oldPassword, newPassword);