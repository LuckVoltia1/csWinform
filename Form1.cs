using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace csWinform{
  class Form1 :Form{
    Label LblUser = new Label();
    Label LblPass = new Label();
    TextBox txtUser = new TextBox();
    TextBox txtPass = new TextBox();
    Button btnLogin = new Button();
    DataGridView gridView = new DataGridView();
    Button btnUpdate = new Button();
    Button btnDelete = new Button();
    Button btnSearch = new Button();
    TextBox txtSearch = new TextBox();


public Form1(){
    InitializeComponent();
      InitializeDataGridView();
        ShowData();
}

  public void InitializeDataGridView()
    {
        gridView.AutoSize = true;
        gridView.Location = new System.Drawing.Point(10, 200);
        this.Controls.Add(gridView);
    }
     public void InitializeComponent()
    {

        this.AutoSize = true;
        // Set properties for Username label
        LblUser.Text = "Username";
        LblUser.Location = new System.Drawing.Point(10, 10);
        LblUser.AutoSize = true;
        this.Controls.Add(LblUser);

        txtSearch.Location = new System.Drawing.Point(400, 10);
        txtSearch.AutoSize = true;
        this.Controls.Add(txtSearch);

        btnSearch.Text = "Search";
        btnSearch.Location = new System.Drawing.Point(600, 10);
        btnSearch.AutoSize = true;
        btnSearch.Click += new EventHandler(btnSearch_Click);
        this.Controls.Add(btnSearch);

        // Set properties for Password label
        LblPass.Text = "Password";
        LblPass.Location = new System.Drawing.Point(10, 40);
        LblPass.AutoSize = true;
        this.Controls.Add(LblPass);

        // Set properties for Username textbox
        txtUser.Size = new System.Drawing.Size(150, 20);
        txtUser.Location = new System.Drawing.Point(150, 10);
        this.Controls.Add(txtUser);

        // Set properties for Password textbox
        txtPass.Size = new System.Drawing.Size(150, 20);
        txtPass.Location = new System.Drawing.Point(150, 40);
        this.Controls.Add(txtPass);

        // Set properties for Login button
        btnLogin.Text = "Login";
        btnLogin.Location = new System.Drawing.Point(100, 100);
        btnLogin.AutoSize = true;
        btnLogin.Click += new EventHandler(btnLogin_Click);
        this.Controls.Add(btnLogin);

        btnUpdate.Text = "Update";
        btnUpdate.Location = new System.Drawing.Point(200, 100);
        btnUpdate.AutoSize = true;
        btnUpdate.Click += new EventHandler(UpdateData);
        this.Controls.Add(btnUpdate);

        btnDelete.Text = "Delete";
        btnDelete.Location = new System.Drawing.Point(300, 100);
        btnDelete.AutoSize = true;
        btnDelete.Click += new EventHandler(DeleteData);
        this.Controls.Add(btnDelete);
    }

    // Event handler for Login button click
    public void btnLogin_Click(object obj, EventArgs args)
    {
        try
        {
            string ab = "server=localhost;Database=loggin;User ID=root;Password=;";
            MySqlConnection con = new MySqlConnection(ab);
                con.Open();
                string query = "Insert into loggin(user, pass) values(@name, @pass)";
                MySqlCommand command = new MySqlCommand(query, con);
                    
                    if(!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPass.Text)){

                        string regValue = "^[a-zA-Z0-9]+@[a-zA-Z]+\\.[a-zA-Z]{2,}$";




                        if(Regex.IsMatch(txtUser.Text,regValue)){

   command.Parameters.AddWithValue("@name", txtUser.Text);
                    command.Parameters.AddWithValue("@pass", txtPass.Text);

                    command.ExecuteNonQuery();
                    ShowData();

                    MessageBox.Show("Success login");
                        }
                        else{
                            MessageBox.Show("Invalid Email");
                        }

                    }
                    else{
                        MessageBox.Show("Please fill all fields");
                    }
                 
            
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }

    public void ShowData()
    {
        string ab = "server=localhost;Database=loggin;User ID=root;Password=;";
        using (MySqlConnection con = new MySqlConnection(ab))
        {
            con.Open();
            string query = "select * from loggin";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                DataTable dataTable = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                gridView.DataSource = dataTable;
            }
        }
    }

    public void UpdateData(object obj, EventArgs evg)
    {
        string ab = "server=localhost;Database=loggin;User ID=root;Password=;";
        int id = Convert.ToInt32(gridView.SelectedRows[0].Cells["id"].Value);
        using (MySqlConnection con = new MySqlConnection(ab))
        {
            con.Open();
            string query = "Update loggin Set user = @name, pass = @pass where id = @id";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", txtUser.Text);
                command.Parameters.AddWithValue("@pass", txtPass.Text);

                command.ExecuteNonQuery();
                ShowData();
            }
        }
    }

    public void DeleteData(object obj, EventArgs args)
    {
        string ab = "server=localhost;Database=loggin;User ID=root;Password=;";
        int id = Convert.ToInt32(gridView.SelectedRows[0].Cells["id"].Value);
        using (MySqlConnection con = new MySqlConnection(ab))
        {
            con.Open();
            string query = "Delete from loggin where id = @id";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                ShowData();
            }
        }
    }

    public void btnSearch_Click(object obj, EventArgs args)
    {
        string ab = "server=localhost;Database=loggin;User ID=root;Password=;";
        using (MySqlConnection con = new MySqlConnection(ab))
        {
            con.Open();
            string query = "select * from loggin where user like @name";
            using (MySqlCommand command = new MySqlCommand(query, con))
            {
                command.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");
                DataTable dataTable = new DataTable();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                gridView.DataSource = dataTable;
            }
        }
    }

}  
}

