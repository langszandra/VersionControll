﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace hazi_vegleges_Sw9r50
{
    
    public partial class Form1 : Form
    {
        Excel.Application xlApp;
        Excel.Workbook xlWB; 
        Excel.Worksheet xlSheet;

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;
        string[] headers = new string[] {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"};
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();

        }
        private void LoadData()
        {
            Flats = context.Flats.ToList();
        
        }
        private void CreateTable()
        {
            


            for (int i = 1; i < headers.Length; i++)
            {
                xlSheet.Cells[1, 1] = headers[0];
                xlSheet.Cells[1, 2] = headers[1];
                xlSheet.Cells[1, 3] = headers[2];
                xlSheet.Cells[1, 4] = headers[3];
                xlSheet.Cells[1, 5] = headers[4];
                xlSheet.Cells[1, 6] = headers[5];
                xlSheet.Cells[1, 7] = headers[6];
                xlSheet.Cells[1, 8] = headers[7];
                xlSheet.Cells[1, 9] = headers[8];
            }

            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 8] = "";
                counter++;

            }
            xlSheet.get_Range(
                         GetCell(2, 1),
                         GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

        }
        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void CreateExcel()
        {
            try
            {
                
                xlApp = new Excel.Application();

               
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                
                xlSheet = xlWB.ActiveSheet;

                
                CreateTable(); 

                
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) 
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, headers.Length));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

           /* int lastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range tableRange = xlSheet.get_Range(GetCell(2, 1), lastRowID);
            tableRange.Font.Bold = true;*/

        }

    }
}
