using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DbClassGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UserIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UserIdTextBox.Enabled = PasswordTextBox.Enabled = UserIdCheckBox.Checked;
            if (UserIdCheckBox.Checked)
                UserIdCheckBox.Focus();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            using (var connection = new SqlConnection(GetConnectionString("master")))
            {
                connection.Open();
                var command = new SqlCommand(" SELECT * FROM sys.databases", connection);
                var reader = command.ExecuteReader();
                DatabasesComboBox.Items.Clear();
                while (reader.Read())
                {
                    DatabasesComboBox.Items.Add(reader["name"]);
                }
                DatabasesComboBox.Enabled = true;
            }
        }

        private string GetConnectionString(string databaseName)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = DataSourceTextBox.Text;
            connectionStringBuilder.InitialCatalog = databaseName;
            connectionStringBuilder.MultipleActiveResultSets = true;
            if (UserIdCheckBox.Checked)
            {
                connectionStringBuilder.IntegratedSecurity = false;
                connectionStringBuilder.UserID = UserIdTextBox.Text;
                connectionStringBuilder.Password = PasswordTextBox.Text;
            }
            else
                connectionStringBuilder.IntegratedSecurity = true;
            return connectionStringBuilder.ConnectionString;
        }

        private void DatabasesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var connectionString = GetConnectionString(DatabasesComboBox.SelectedItem.ToString());
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.TABLES", connection);
                var reader = command.ExecuteReader();
                TablesCheckedListBox.Items.Clear();
                while (reader.Read())
                {
                    var schema = reader["TABLE_SCHEMA"];
                    var table = reader["TABLE_NAME"];
                    TablesCheckedListBox.Items.Add(schema + "." + table);
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;

            foreach (var item in TablesCheckedListBox.CheckedItems)
            {
                var text = item.ToString();
                var schema = text.Split('.')[0];
                var table = text.Split('.')[1];
                var columns = GetTableColumns(schema, table);

                GenerateEntities(folderBrowser.SelectedPath, schema, table, columns);
                GenerateRepositoryInterfaces(folderBrowser.SelectedPath , schema , table , columns );
                GenerateRepositories(folderBrowser.SelectedPath , schema , table , columns );
            }
        }

        private void GenerateRepositories(string generatePath, string schema, string table, List<ColumnModel> columns)
        {
            var entitiesFolder = Path.Combine(generatePath, "Repositories");
            if (!Directory.Exists(entitiesFolder))
                Directory.CreateDirectory(entitiesFolder);

            List<string> classLines = new List<String>();
            classLines.Add("using System;");
            classLines.Add("using System.Collections.Generic;");
            classLines.Add("using System.Data.SqlClient;");
            classLines.Add("");
            classLines.Add("namespace " + RootNameSpaceTextBox.Text + ".Repositories");
            classLines.Add("{");
            classLines.Add("    public class " + table + "Repository : DataLayer.GenericRepository<Entities." + GetSingularName(table) + ">,RepositoryAbstracts.I"+ table +"Repository");
            classLines.Add("    {");
            classLines.Add("        public "+ table +"Repository() : base(\"name=DbConnectionString\") { }");
            foreach (var column in columns)
            {
                var dataType = ConvertSqlTypeClr(column.DataType, column.IsNUllable);
                classLines.Add("        public List<Entities."+ GetSingularName(table) +"> GetBy"+column.Name + "(" + ConvertSqlTypeClr(column.DataType, column.IsNUllable) + " value)");
                classLines.Add("        {");
                if (dataType == "string")
                {
                    classLines.Add("            return RunQuery(\"SELECT * FROM ["+ schema +"].[" + table + "] WHERE [" + column.Name + "] LIKE @Value\", new SqlParameter(\"Value\", value));");
                }
                else
                {
                    classLines.Add("            return RunQuery(\"SELECT * FROM ["+ schema +"].[" + table + "] WHERE [" + column.Name + "] = @Value\", new SqlParameter(\"Value\", value));");
                }
                //classLines.Add("        List<Entities." + GetSingularName(table) + "> GetBy" + column.Name + "(" + ConvertSqlTypeClr(column.DataType, column.IsNUllable) + " value);");
                classLines.Add("        }");
            }
            classLines.Add("    }");
            classLines.Add("}");

            File.WriteAllLines(Path.Combine(entitiesFolder, GetSingularName(table) + ".cs"), classLines);
        }

        private void GenerateRepositoryInterfaces(string generatePath, string schema, string table, List<ColumnModel> columns)
        {
            var entitiesFolder = Path.Combine(generatePath, "Abstracts");
            if (!Directory.Exists(entitiesFolder))
                Directory.CreateDirectory(entitiesFolder);

            List<string> classLines = new List<String>();
            classLines.Add("using System;");
            classLines.Add("using System.Collections.Generic;");
            classLines.Add("");
            classLines.Add("namespace " + RootNameSpaceTextBox.Text + ".RepositoryAbstracts");
            classLines.Add("{");
            classLines.Add("    public interface I" + table + "Repository : DataLayer.IRepository<Entities." + GetSingularName(table) + ">");
            classLines.Add("    {");

            foreach (var column in columns)
            {
                classLines.Add("        List<Entities."+ GetSingularName(table) +"> GetBy"+ column.Name + "(" + ConvertSqlTypeClr(column.DataType, column.IsNUllable)+ " value);");
            }
            classLines.Add("    }");
            classLines.Add("}");

            File.WriteAllLines(Path.Combine(entitiesFolder, GetSingularName(table) + ".cs"), classLines);
        }

        private void GenerateEntities(string generatePath, string schema, string table, List<ColumnModel> columns)   
        {
            var entitiesFolder = Path.Combine(generatePath, "Entities");
            if (!Directory.Exists(entitiesFolder))
                Directory.CreateDirectory(entitiesFolder);
            List<string> classLines = new List<String>();
            classLines.Add("using System;");
            classLines.Add("");
            classLines.Add("namespace " + RootNameSpaceTextBox.Text + ".Entities");
            classLines.Add("{");
            classLines.Add("    [DataLayer.Table(\"" + schema + "\",\"" + table + "\")]");
            classLines.Add("    public class " + GetSingularName(table));
            classLines.Add("    {");

            foreach (var column in columns)
            {
                if (column.IsPriamaryKey)
                {
                    classLines.Add("        [DataLayer.PrimaryKey]");
                }
                if (column.IsComputed)
                {
                    classLines.Add("        [DataLayer.ComputedColumn]");
                }
                classLines.Add("        public " + ConvertSqlTypeClr(column.DataType, column.IsNUllable) + " " + column.Name + " { get; set; }");
            }
            classLines.Add("    }");
            classLines.Add("}");

            File.WriteAllLines(Path.Combine(entitiesFolder, GetSingularName(table) + ".cs"), classLines);
        }

        private string ConvertSqlTypeClr(string type, bool nullable)
        {
            switch (type)
            {
                case "int":
                    return nullable ? "int?" : "int";

                case "bigint":
                    return nullable ? "long?" : "long";

                case "datetime":
                case "date":
                case "datetime2":
                    return nullable ? "DateTime?" : "DateTime";

                case "nvarchar":
                case "nchar":
                case "varchar":
                case "char":
                    return "string";

                case "bit":
                    return nullable ? "bool?" : "bool";

                case "binary":
                case "image":
                    return "byte[]";

                case "decimal":
                    return nullable ? "decimal?" : "decimal";

                case "float":
                    return nullable ? "float?" : "float";
            }
            return "object";
        }

        private string GetSingularName(string name)
        {
            if (name.EndsWith("ies"))
            {
                return name.Substring(0, name.Length - 3) + "y";
            }
            return name.Substring(0, name.Length - 1);
        }

        private List<ColumnModel> GetTableColumns(string schema, string tableName)
        {
            var columns = new List<ColumnModel>();

            using (var connection = new SqlConnection(GetConnectionString(DatabasesComboBox.SelectedItem.ToString())))
            {
                connection.Open();
                List<string> primaryKeyColumns = new List<string>();
                var keysCommand = new SqlCommand("SELECT tc.TABLE_SCHEMA , tc.TABLE_NAME , ccu.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc inner Join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu on ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME WHERE tc.TABLE_SCHEMA = N'" + schema + "' AND tc.TABLE_NAME = N'" + tableName + "' AND tc.CONSTRAINT_TYPE = N'PRIMARY KEY'", connection);
                var KeysReader = keysCommand.ExecuteReader();
                while (KeysReader.Read())
                {
                    primaryKeyColumns.Add(KeysReader["COLUMN_NAME"].ToString());
                }
                List<string> computedColumns = new List<string>();
                var computedCommand = new SqlCommand("SELECT [name] FROM sys.columns WHERE object_id = object_id('" + schema + "." + tableName + "') AND (is_identity = 1 OR is_computed = 1)", connection);
                var computedReader = computedCommand.ExecuteReader();
                while (computedReader.Read())
                {
                    computedColumns.Add(computedReader["name"].ToString());
                }
                var command = new SqlCommand("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = N'" + schema + "' AND TABLE_NAME = N'" + tableName + "'", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var columnModel = new ColumnModel()
                    {
                        IsPriamaryKey = primaryKeyColumns.Any(col => col.Equals(reader["COLUMN_NAME"])),
                        IsComputed = computedColumns.Any(col => col.Equals(reader["COLUMN_NAME"])),
                        Name = reader["COLUMN_NAME"].ToString(),
                        DataType = reader["DATA_TYPE"].ToString(),
                        IsNUllable = reader["IS_NULLABLE"].ToString() == "YES"
                    };
                    columns.Add(columnModel);
                }
            }

            return columns;
        }
    }
    public class ColumnModel
    {
        public string Name { get; set; }
        public string DataType { get; set; }

        public bool IsPriamaryKey { get; set; }
        public bool IsComputed { get; set; }
        public bool IsNUllable { get; set; }
    }
}
