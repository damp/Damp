﻿#region

using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DampServer.interfaces;
using DampServer.responses;

#endregion

namespace DampServer.commands
{
    /**
     * LoginCommand
     * 
     */

    public class LoginCommand : IServerCommand
    {
        public LoginCommand()
        {
            NeedsAuthcatication = false;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("Login");
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            if (string.IsNullOrEmpty(http.Query.Get("Username")) || string.IsNullOrEmpty(http.Query.Get("Password")))
            {
                http.SendXmlResponse(new ErrorXmlResponse {Message = "Missing arguments #1112!"});
                return;
            }

            string username = http.Query.Get("Username");
            string password = http.Query.Get("Password");

            Database db = new Database();

            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE username LIKE @username AND password LIKE @password";
            sqlCmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = http.Query.Get("Username");
            sqlCmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = http.Query.Get("Password");

            SqlDataReader r;

            try
            {
                r = sqlCmd.ExecuteReader();
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(e.Message);
                http.SendXmlResponse(new ErrorXmlResponse {Message = "Internal Server Error!! #90022"});
                return;
            }

            r.Read();

            if (r.HasRows)
            {
                SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(username + password));
                string delimitedHexHash = BitConverter.ToString(hash);
                string hexHash = delimitedHexHash.Replace("-", "");


                Database db2 = new Database();
                db2.Open();
                SqlCommand sqlCmdUpdate = db2.GetCommand();
                sqlCmdUpdate.CommandText = "UPDATE Users SET authToken = @authToken WHERE userid = @userid";
                sqlCmdUpdate.Parameters.Add("@authToken", SqlDbType.NVarChar).Value = hexHash;
                sqlCmdUpdate.Parameters.Add("@userid", SqlDbType.BigInt).Value = (long) r["userid"];

                sqlCmdUpdate.ExecuteNonQuery();
                db2.Close();

                http.SendXmlResponse(new StatusXmlResponse {Code = 200, Message = hexHash, Command = "Login"});
            }
            else
            {
                http.SendXmlResponse(new StatusXmlResponse
                    {
                        Message = "Wrong username or password",
                        Code = 301,
                        Command = "Login"
                    });
            }

            r.Close();
            db.Close();
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}