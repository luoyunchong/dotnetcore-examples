using System.Data.SQLite;

static void Open()
{
    string baseConnectionString = "Data Source=local.db";
    var connectionString = new SQLiteConnectionStringBuilder(baseConnectionString)
    {
        Password = "123qwe"
    }.ToString();

    using SQLiteConnection? connection = new SQLiteConnection(connectionString);
    connection.Open();
}


Open();