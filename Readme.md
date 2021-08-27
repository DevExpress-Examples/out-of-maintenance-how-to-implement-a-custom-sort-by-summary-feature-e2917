<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128582106/10.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2917)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/WindowsApplication53/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication53/Form1.vb))
* [Program.cs](./CS/WindowsApplication53/Program.cs) (VB: [Program.vb](./VB/WindowsApplication53/Program.vb))
<!-- default file list end -->
# How to implement a custom Sort By Summary feature


<p>The <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldBase_SortBySummaryInfotopic">PivotGridFieldBase.SortBySummaryInfo</a> property allows sorting data by displayed cell values and by using a custom summary algorithm. In the first case, it is necessary to specify the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldSortBySummaryInfo_Fieldtopic">PivotGridFieldSortBySummaryInfo.Field</a> property to sort data by fields summary values. In the second case, it is necessary to use the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldSortBySummaryInfo_FieldNametopic">PivotGridFieldSortBySummaryInfo.FieldName</a> property to select a data source field that should be used to calculate summaries. In the latter case, you can select a summary type via the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldSortBySummaryInfo_SummaryTypetopic">PivotGridFieldSortBySummaryInfo.SummaryType</a> property. <br />
Built-in popup menu allow applying the Sort By Summary  feature by using the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldSortBySummaryInfo_Fieldtopic">PivotGridFieldSortBySummaryInfo.Field</a> property. This example demonstrates how to customize such a menu to provide the Sort By Summary feature using the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridFieldSortBySummaryInfo_FieldNametopic">PivotGridFieldSortBySummaryInfo.FieldName</a> property.</p>

<br/>


