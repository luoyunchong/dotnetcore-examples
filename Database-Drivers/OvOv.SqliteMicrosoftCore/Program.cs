using Microsoft.Data.Sqlite;

static void Open()
{
    string baseConnectionString = "Data Source=local.db";
    var connectionString = new SqliteConnectionStringBuilder(baseConnectionString)
    {
        Mode = SqliteOpenMode.ReadWriteCreate,
        Password = "123qwe"
    }.ToString();

    using SqliteConnection? connection = new SqliteConnection(connectionString);
    connection.Open();
}


Open();