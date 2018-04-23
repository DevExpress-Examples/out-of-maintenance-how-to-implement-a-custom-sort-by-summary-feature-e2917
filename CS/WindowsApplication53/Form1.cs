using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;

namespace WindowsApplication53
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTable();
            pivotGridControl1.RefreshData();
            pivotGridControl1.BestFit();

            this.pivotGridControl1.PopupMenuShowing += new DevExpress.XtraPivotGrid.PopupMenuShowingEventHandler(this.pivotGridControl1_PopupMenuShowing);
        }
        private void PopulateTable()
        {
            DataTable myTable = dataSet1.Tables["Data"];
            myTable.Rows.Add(new object[] { "Xxx", DateTime.Today, 7 });
            myTable.Rows.Add(new object[] { "Xxx", DateTime.Today.AddDays(1), 4 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 14 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today, 11 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 10 });

            myTable.Rows.Add(new object[] { "Xxx", DateTime.Today.AddYears(1), 4 });
            myTable.Rows.Add(new object[] { "Xxx", DateTime.Today.AddYears(1).AddDays(1), 2 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 3 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 2 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 1 });

            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 0 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 0 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 0 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 0 });

            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 0 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 3 });


            
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                fieldName.SortBySummaryInfo.FieldName = "Value";
                fieldName.SortBySummaryInfo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;
            }
            else
                fieldName.SortBySummaryInfo.Reset(); 
        }

        private void pivotGridControl1_CustomSummary(object sender, DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs e)
        {
            e.CustomValue = e.CreateDrillDownDataSource().RowCount;
        }

        private void pivotGridControl1_PopupMenuShowing(object sender, DevExpress.XtraPivotGrid.PopupMenuShowingEventArgs e)
        {
            if (! Equals( e.MenuType, PivotGridMenuType.FieldValue)) return;
            foreach (DXMenuItem item in e.Menu.Items)
            {
                PivotGridFieldPair pair = item.Tag as PivotGridFieldPair;
                if (pair != null)
                {
                    item.Click += new EventHandler(item_Click);
                    item.Caption = "Custom " + item.Caption;
                    CustomSortBySummaryTag tag = new CustomSortBySummaryTag();
                    tag.Pair = pair;
                    tag.Condition  = e.HitInfo.ValueInfo.Data.VisualItems.GetFieldSortConditions(e.HitInfo.ValueInfo.IsColumn, e.HitInfo.ValueInfo.Item.Index);
                    item.Tag = tag;
                }
            }
        }

        

        void item_Click(object sender, EventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            if (item == null ) return ;
            CustomSortBySummaryTag tag = item.Tag as CustomSortBySummaryTag;
            if (tag != null)
                SetFieldSortBySummary(tag.Pair.Field, tag.Pair.DataField, tag.Condition, item.Checked);
        }

        private void SetFieldSortBySummary(PivotGridFieldBase field, PivotGridFieldBase dataField, List<PivotGridFieldSortCondition> condition, bool sort)
        {
            if (!field.CanSortBySummary) return;
            if (sort)
            {
                field.SortBySummaryInfo.FieldName = dataField.FieldName ;
                field.SortBySummaryInfo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
                field.SortBySummaryInfo.Conditions.Clear();
                field.SortBySummaryInfo.Conditions.AddRange(condition);
            }
            else
            {
                field.SortBySummaryInfo.Reset();
            }
        }

        private void pivotGridControl1_MenuItemClick(object sender, PivotGridMenuItemClickEventArgs e)
        {
            DXMenuCheckItem item = sender as DXMenuCheckItem;
            if ( Equals( item,  null ) ) return ;
            CustomSortBySummaryTag tag = item.Tag as CustomSortBySummaryTag;
            if ( Equals( tag , null))
                e.Allow = false;
        }

    }

    public class CustomSortBySummaryTag
    {
        PivotGridFieldPair _pair;
        List<PivotGridFieldSortCondition> _condition;
        public CustomSortBySummaryTag() { _condition = new List<PivotGridFieldSortCondition>(); }
        public PivotGridFieldPair Pair { get { return _pair; } set { _pair = value; } }
        public List<PivotGridFieldSortCondition> Condition { get { return _condition; } set { _condition = value; } }
    }
}