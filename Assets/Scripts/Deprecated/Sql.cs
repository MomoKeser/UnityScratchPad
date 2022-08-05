using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
 
public class Sql : MonoBehaviour//Component
{
    SqlConnection connection;
    string connectionString = "Data Source=MOMO;Database=GameDB;User ID=sa;Password=bro";
    public UnityEngine.UI.InputField usernameText;

    private void Awake()//Start()
    {
        connection = new SqlConnection(connectionString);
        connection.Open();
    }

    void OnDestroy()
    {
        connection?.Close();
    }

    private void PrintAllPlayers()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM players", connection);
        int result = command.ExecuteNonQuery();
        SqlDataReader reader = command.ExecuteReader();
        while(reader.Read())
        {
            Debug.Log($"Username: {reader["username"]} - Id: {reader["id"]}");
        }
    }

    private void CreateNewPlayer()
    {
        SqlCommand command = new SqlCommand("INSERT INTO players(username) VALUES(\'" + usernameText.text + "\')", connection);
        int result = command.ExecuteNonQuery();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateNewPlayer();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PrintAllPlayers();
        }

    }
}
