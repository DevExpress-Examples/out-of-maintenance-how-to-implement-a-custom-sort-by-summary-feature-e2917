Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPivotGrid.Data

Namespace WindowsApplication53
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub
        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            PopulateTable()
            pivotGridControl1.RefreshData()
            pivotGridControl1.BestFit()

            AddHandler pivotGridControl1.PopupMenuShowing, AddressOf pivotGridControl1_PopupMenuShowing
        End Sub
        Private Sub PopulateTable()
            Dim myTable As DataTable = dataSet1.Tables("Data")
            myTable.Rows.Add(New Object() {"Xxx", Date.Today, 7})
            myTable.Rows.Add(New Object() {"Xxx", Date.Today.AddDays(1), 4})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today, 12})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1), 14})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today, 11})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1), 10})

            myTable.Rows.Add(New Object() {"Xxx", Date.Today.AddYears(1), 4})
            myTable.Rows.Add(New Object() {"Xxx", Date.Today.AddYears(1).AddDays(1), 2})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddYears(1), 3})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1).AddYears(1), 1})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddYears(1), 2})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1).AddYears(1), 1})

            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddYears(1), 0})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1).AddYears(1), 0})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddYears(1), 0})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1).AddYears(1), 0})

            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddYears(1), 0})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1).AddYears(1), 3})

        End Sub

        Private Sub pivotGridControl1_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGridControl1.CustomSummary
            e.CustomValue = e.CreateDrillDownDataSource().RowCount
        End Sub

        Private Sub pivotGridControl1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PopupMenuShowingEventArgs)
            If Not Equals(e.MenuType, PivotGridMenuType.FieldValue) Then
                Return
            End If
            For Each item As DXMenuItem In e.Menu.Items
                Dim pair As PivotGridFieldPair = TryCast(item.Tag, PivotGridFieldPair)
                If pair IsNot Nothing Then
                    AddHandler item.Click, AddressOf item_Click
                    item.Caption = "Custom " & item.Caption

                    Dim tag_Renamed As New CustomSortBySummaryTag()
                    tag_Renamed.Pair = pair
                    tag_Renamed.Condition = e.HitInfo.ValueInfo.Item.GetFieldSortConditions()
                    item.Tag = tag_Renamed
                End If
            Next item
        End Sub



        Private Sub item_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim item As DXMenuCheckItem = TryCast(sender, DXMenuCheckItem)
            If item Is Nothing Then
                Return
            End If

            Dim tag_Renamed As CustomSortBySummaryTag = TryCast(item.Tag, CustomSortBySummaryTag)
            If tag_Renamed IsNot Nothing Then
                SetFieldSortBySummary(tag_Renamed.Pair.FieldItem, tag_Renamed.Pair.DataFieldItem, tag_Renamed.Condition, item.Checked)
            End If
        End Sub

        Private Sub SetFieldSortBySummary(ByVal fieldItem As PivotFieldItemBase, ByVal dataFieldItem As PivotFieldItemBase, ByVal condition As List(Of PivotGridFieldSortCondition), ByVal sort As Boolean)
            'The following code can be used to access the actual PivotGridField object. The PivotFieldItem class Is used to support asynchronous operations.
            'Dim field As PivotGridField = CType(pivotGridControl1, IPivotGridViewInfoDataOwner).DataViewInfo.GetField(fieldItem)


            If Not fieldItem.CanSortBySummary Then
                Return
            End If
            If sort Then
                fieldItem.SortBySummaryInfo.FieldName = dataFieldItem.FieldName
                fieldItem.SortBySummaryInfo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count
                fieldItem.SortBySummaryInfo.Conditions.Clear()
                fieldItem.SortBySummaryInfo.Conditions.AddRange(condition)
            Else
                fieldItem.SortBySummaryInfo.Reset()
            End If
        End Sub

        Private Sub pivotGridControl1_MenuItemClick(ByVal sender As Object, ByVal e As PivotGridMenuItemClickEventArgs) Handles pivotGridControl1.MenuItemClick
            Dim item As DXMenuCheckItem = TryCast(sender, DXMenuCheckItem)
            If Equals(item, Nothing) Then
                Return
            End If

            Dim tag_Renamed As CustomSortBySummaryTag = TryCast(item.Tag, CustomSortBySummaryTag)
            If Equals(tag_Renamed, Nothing) Then
                e.Allow = False
            End If
        End Sub

        Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
            fieldName.SortBySummaryInfo.Reset()

            fieldName.SortBySummaryInfo.FieldName = "Value"
            fieldName.SortBySummaryInfo.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom
        End Sub
    End Class

    Public Class CustomSortBySummaryTag
        Private _pair As PivotGridFieldPair
        Private _condition As List(Of PivotGridFieldSortCondition)
        Public Sub New()
            _condition = New List(Of PivotGridFieldSortCondition)()
        End Sub
        Public Property Pair() As PivotGridFieldPair
            Get
                Return _pair
            End Get
            Set(ByVal value As PivotGridFieldPair)
                _pair = value
            End Set
        End Property
        Public Property Condition() As List(Of PivotGridFieldSortCondition)
            Get
                Return _condition
            End Get
            Set(ByVal value As List(Of PivotGridFieldSortCondition))
                _condition = value
            End Set
        End Property
    End Class
End Namespace