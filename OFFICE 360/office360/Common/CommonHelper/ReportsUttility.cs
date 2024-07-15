using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using office360.Models.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using static office360.Models.General.Constants;

namespace office360.Common.CommonHelper
{
    public class ReportsUttility
    {
        #region REPORT PATH
        public static class ReportPath
        {
            #region PREVIOUS
            public static string EmptyReport = "EmptyReport.rpt";
            #endregion

            #region UMAIR PATH
            private static string AccountReports= HttpContext.Current.Server.MapPath("~/Areas/AccReports/CRReport/Accounts/");
            #endregion
            #region UMAIR PATH
            public static string ClassFeeStructureReport = AccountReports + "RP.rpt";
            #endregion

        }

        internal static void CreateReport(string FullReportPath, DataSet ds, _SqlParameters PostedData, Controller ReportController)
        {
            int statuscode = 200;
            ReportController.Response.Clear();
            ReportController.Response.ContentType = "application/pdf";
            try
            {
                if (ds != null)
                {
                    ds.Tables.Add(GlobalUtilities.TableGeneric());
                    // #if (DEBUG)
                   // CreateSchemaFile("ClassFeeStructureReport.xml", ds);   // for create xml file schema
                    // #endif
                    ExportReport(FullReportPath, PostedData.ExportType ?? 1, ds);
                }
                else
                {
                    ExportReport(ReportPath.EmptyReport, 1, ds);
                }
            }
            catch (Exception ex)
            {
                statuscode = 103;
                ReportController.Response.StatusCode = statuscode;
            }
        }
        public static void ExportReport(string FullReportPath, int? ExportFormat, DataSet ds)
        {
            string FileName = Path.GetFileNameWithoutExtension(FullReportPath);
            ReportDocument doc = new ReportDocument();
            doc.Load(FullReportPath);
            doc.SetDataSource(ds);

            //foreach (DataTable table in ds.Tables)
            //{
            //    doc.Database.Tables[table.TableName].SetDataSource(table);
            //}

            ExportOptions exportOpts = doc.ExportOptions;
            switch (ExportFormat)
            {
                case 1:
                    exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
                    break;
                case 2:
                    exportOpts.ExportFormatType = ExportFormatType.WordForWindows;
                    break;
                case 3:
                    exportOpts.ExportFormatType = ExportFormatType.ExcelRecord;
                    ExcelFormatOptions options = new ExcelFormatOptions();
                    options.ExcelTabHasColumnHeadings = true;
                    options.ExcelUseConstantColumnWidth = true;
                    exportOpts.ExportFormatOptions = options;
                    options = null;
                    break;
                case 4:
                    exportOpts.ExportFormatType = ExportFormatType.Excel;
                    ExcelFormatOptions options1 = new ExcelFormatOptions();
                    options1.ExcelUseConstantColumnWidth = true;
                    exportOpts.ExportFormatOptions = options1;
                    options1 = null;
                    break;
                case 5:
                    exportOpts.ExportFormatType = ExportFormatType.RichText;
                    break;
                default:
                    exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
                    break;
            }
            try
            {
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                doc.ExportToHttpResponse(exportOpts, HttpContext.Current.Response, false, "" + FileName);
            }
            catch (Exception e)
            {
                //throw;
            }
            finally
            {
                doc.Close();
                doc.Dispose();
                ds.Dispose();
            }
        }

        private static void CreateSchemaFile(string xmlfileName, DataSet ds)
        {
            //string absoluteFileName = ApplicationConstants.SubdomainReports + xmlfileName;
            string absoluteFileName = HttpContext.Current.Server.MapPath("~/Reports/" + xmlfileName);
            FileStream myFileStream = new FileStream(absoluteFileName, FileMode.Create);
            XmlTextWriter myXmlWriter = new XmlTextWriter(myFileStream, Encoding.Unicode);
            ds.WriteXml(myXmlWriter, XmlWriteMode.WriteSchema);
            myXmlWriter.Close();
        }
        #endregion
    }
    public static class GlobalUtilities
    {
        public static DataTable TableGeneric()
        {
            DataTable data = new DataTable();
            DataRow row;
            string Logo = "";

            try
            {
                // Define all columns at the beginning
                data.TableName = "TableGeneric";
                data.Columns.Add("Name");
                data.Columns.Add("UserName");
                data.Columns.Add("Password");
                data.Columns.Add("EmailAddress");
                data.Columns.Add("MobileNumber");
                data.Columns.Add("CompanyName");
                data.Columns.Add("AddressLine");
                data.Columns.Add("PhoneNumber");
                data.Columns.Add("CoyEmailAddress");
                data.Columns.Add("CompanyWebsite");
                data.Columns.Add("CityName");
                data.Columns.Add("CountryName");
                data.Columns.Add("CallingCode");
                data.Columns.Add("BranchAddress");
                data.Columns.Add("BranchName");
                data.Columns.Add("BranchCity");
                data.Columns.Add("img", System.Type.GetType("System.Byte[]")); // Image column

                // Prepare the data row
                row = data.NewRow();
                row["Name"] = Session_Manager.Name;
                row["UserName"] = Session_Manager.UserName;
                row["Password"] = Session_Manager.Password;
                row["EmailAddress"] = Session_Manager.EmailAddress;
                row["MobileNumber"] = Session_Manager.MobileNumber;
                row["CompanyName"] = Session_Manager.CompanyName;
                row["AddressLine"] = Session_Manager.AddressLine;
                row["PhoneNumber"] = Session_Manager.PhoneNumber;
                row["CoyEmailAddress"] = Session_Manager.CoyEmailAddress;
                row["CompanyWebsite"] = Session_Manager.CompanyWebsite;
                row["CityName"] = Session_Manager.CityName;
                row["CountryName"] = Session_Manager.CountryName;
                row["CallingCode"] = Session_Manager.CallingCode;
                row["BranchAddress"] = Session_Manager.BranchAddress;
                row["BranchName"] = Session_Manager.BranchName;
                row["BranchCity"] = Session_Manager.BranchCity;

                // Set logo path
                Logo = (ApplicationConstants.ReportLogo) + Session_Manager.UploadLogo;
                if (!File.Exists(Logo))
                {
                    Logo = (ApplicationConstants.ReportLogo) + "NoImage.jpeg";
                }

                // Load the logo image
                try
                {
                    using (FileStream fs = new FileStream(Logo, FileMode.Open, FileAccess.Read))
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        row["img"] = br.ReadBytes((int)br.BaseStream.Length);
                    }
                }
                catch (Exception Ex)
                {
                    // Consider logging the exception or providing an alternative action
                    throw Ex;
                }

                // Add the row to the DataTable
                data.Rows.Add(row);

                return data;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> list, string TableName = null)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = null;
            if (TableName == null)
            {
                table = new DataTable();
            }
            else
            {
                table = new DataTable(TableName);
            }
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }


    }

}