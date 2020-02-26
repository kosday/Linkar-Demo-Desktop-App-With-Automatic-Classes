using LinkarClient;
using LinkarCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINKARDEMO
{
    public partial class FormDemo : Form
    {
        LinkarClt _LinkarClt;

        public FormDemo()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string entrypoint = txtEntryPoint.Text;
            string host = txtLinkarHost.Text;
            short port = (short)txtLinkarPort.Value;
            string language = txtLanguage.Text;
            string freeText = txtFreeText.Text;

            try
            {
                //Create CredentialsOptions object with connection data
                CredentialsOptions credentialOptions = new CredentialsOptions(host, entrypoint, port, username, password, language, freeText);
                //Create LinkarClt client
                this._LinkarClt = new LinkarClt();
                //Execute client Login
                this._LinkarClt.Login(credentialOptions);
            }
            catch (Exception ex)
            {
                string msgErr = GetExceptionInfo(ex);
                MessageBox.Show(this, msgErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtEntryPoint.Enabled = false;
            txtLinkarHost.Enabled = false;
            txtLinkarPort.Enabled = false;
            txtLanguage.Enabled = false;
            txtFreeText.Enabled = false;
            btnLogin.Enabled = false;

            btnLogout.Enabled = true;
            btnCustomers.Enabled = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                //Close the client object connection
                _LinkarClt.Logout();
            }
            catch (Exception ex)
            {
                string msgErr = GetExceptionInfo(ex);
                MessageBox.Show(this, msgErr, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnLogout.Enabled = false;
            btnCustomers.Enabled = false;

            btnLogin.Enabled = true;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtEntryPoint.Enabled = true;
            txtLinkarHost.Enabled = true;
            txtLinkarPort.Enabled = true;
            txtLanguage.Enabled = true;
            txtFreeText.Enabled = true;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            //Open Customers Form, the client object is passed as a parameter.
            FormCustomers frm = new FormCustomers(this._LinkarClt);
            frm.ShowDialog();
        }

        public static string GetExceptionInfo(Exception ex)
        {
            string msg = "";
            if (ex.GetType() == typeof(LkException))
            {
                LkException lkex = (LkException)ex;
                msg = "LINKAR EXCEPTION ERROR";
                if (lkex.ErrorCode == LkException.ERRORCODE.C0003)
                    msg += "\r\nERROR CODE: " + lkex.ErrorCode +
                           "\r\nERROR MESSAGE: " + lkex.ErrorMessage +
                           "\r\nInternal ERROR CODE: " + lkex.InternalCode +
                           "\r\nInternal ERROR MESSAGE: " + lkex.InternalMessage;
                else
                    msg += "\r\nERROR CODE: " + lkex.ErrorCode +
                           "\r\nERROR MESSAGE: " + lkex.ErrorMessage;
            }
            else if (ex.GetType() == typeof(System.Net.Sockets.SocketException))
            {
                System.Net.Sockets.SocketException soex = (System.Net.Sockets.SocketException)ex;
                msg = "SOCKET EXCEPTION ERROR\r\n" + soex.Message;
            }
            else
            {
                msg = "EXCEPTION ERROR\r\n" + ex.Message;
            }

            return msg;
        }

    }
}
