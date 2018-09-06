<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="BomChange.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Charts/Chart.budle.js"></script>
    <script src="Charts/utils.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<div id="container" style="width: 75%;">
		<canvas id="canvas"></canvas>
	</div>



	<script>
        var MONTHS = []
        var NUM = []
        // alert(' GetDatas() ')
        $.ajax(
            {
                url: 'Handler2.ashx',
                type: 'post',
                dataType: 'json',
                error: function (data) { alert('失败'); },
                success: function (data) {
                    //alert(data);
                    //alert(data.length)
                    $.each(data, function (k, v) {
                        //alert(v.Product_Name)
                        MONTHS[k] = v.Product_Name
                        NUM[k] = v.id;
                    })
                    // alert(MONTHS)
                    getvalue(MONTHS, NUM)

                }

            }
        )

        function getvalue(arr, numY) {
            //var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            var color = Chart.helpers.color;
            var barChartData = {
                labels: arr,
                datasets: [{
                    label: '物料数量',
                    backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.red,
                    borderWidth: 1,
                    data: numY,
                }]

            }
			var ctx = document.getElementById('canvas').getContext('2d');
			window.myBar = new Chart(ctx, {
				type: 'bar',
				data: barChartData,
				options: {
					responsive: true,
					legend: {
						position: 'top',
					},
					title: {
						display: true,
						text: '人员物料更新统计'
					}
				}
			});

        }
      


	</script>



</asp:Content>
