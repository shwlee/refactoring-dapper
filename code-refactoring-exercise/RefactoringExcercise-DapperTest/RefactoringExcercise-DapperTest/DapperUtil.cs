using Dapper;
using System.Data.SqlClient;

namespace RefactoringExcercise_DapperTest;

public static class DapperUtil
{
    public static Task<IEnumerable<WeatherForecast>> GetOver30CWheathers(IConfiguration config)
    {   
        var selectWeatherSql = "SELECT WH.*\r\nFROM WeatherHistory AS WH\r\nINNER JOIN Area AS A ON A.AreaCode = WH.AreaCode\r\nWHERE WH.TemperatureC > 30";
        var connectionString = config.GetConnectionString("DefaultConnection");
        var connection = new SqlConnection(connectionString);
        connection.Open();
        var result = connection.QueryAsync<WeatherForecast>(selectWeatherSql);
        return result;
    }
}
