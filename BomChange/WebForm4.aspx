<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="BomChange.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Charts/Chart.budle.js"></script>
    <script src="Charts/utils.js"></script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div id="container" style="width: 75%;">
        <canvas id="canvas"></canvas>
    </div>
    <input type="button" onclick="GetDatas()" value="获取" />
    <button id="randomizeData">Randomize Data</button>
    <button id="addDataset">Add Dataset</button>
    <button id="removeDataset">Remove Dataset</button>
    <button id="addData">Add Data</button>
    <button id="removeData">Remove Data</button>
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
                        NUM[k] = v.p2;
                    })
                    // alert(MONTHS)
                    getdata(MONTHS, NUM)

                }

            }
        )

        function getdata(arr, numY) {
            //alert(MONTHS)
            //var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            //var MONTHS=arr
            //alert(arr)
            var color = Chart.helpers.color;
            barChartData = {
                labels: arr,
                datasets: [{
                    label: 'Dataset 1',
                    backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.red,
                    borderWidth: 1,
                    data: numY,
                    //data: [
                    //	randomScalingFactor(),
                    //	randomScalingFactor(),
                    //	randomScalingFactor(),
                    //	randomScalingFactor(),
                    //	randomScalingFactor(),
                    //	randomScalingFactor(),
                    //	randomScalingFactor()
                    //]
                }, {
                    label: 'Dataset 2',
                    backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.blue,
                    borderWidth: 1,
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor()
                    ]
                }]

            };

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
                        text: 'Chart.js Bar Chart'
                    }
                }
            });
        }


        document.getElementById('randomizeData').addEventListener('click', function () {
            var zero = Math.random() < 0.2 ? true : false;
            barChartData.datasets.forEach(function (dataset) {
                dataset.data = dataset.data.map(function () {
                    return zero ? 0.0 : randomScalingFactor();
                });

            });
            window.myBar.update();
        });

        var colorNames = Object.keys(window.chartColors);
        document.getElementById('addDataset').addEventListener('click', function () {
            var colorName = colorNames[barChartData.datasets.length % colorNames.length];
            var dsColor = window.chartColors[colorName];
            var newDataset = {
                label: 'Dataset ' + (barChartData.datasets.length + 1),
                backgroundColor: color(dsColor).alpha(0.5).rgbString(),
                borderColor: dsColor,
                borderWidth: 1,
                data: []
            };

            for (var index = 0; index < barChartData.labels.length; ++index) {
                newDataset.data.push(randomScalingFactor());
            }

            barChartData.datasets.push(newDataset);
            window.myBar.update();
        });

        document.getElementById('addData').addEventListener('click', function () {
            if (barChartData.datasets.length > 0) {
                var month = MONTHS[barChartData.labels.length % MONTHS.length];
                barChartData.labels.push(month);

                for (var index = 0; index < barChartData.datasets.length; ++index) {
                    // window.myBar.addData(randomScalingFactor(), index);
                    barChartData.datasets[index].data.push(randomScalingFactor());
                }

                window.myBar.update();
            }
        });

        document.getElementById('removeDataset').addEventListener('click', function () {
            barChartData.datasets.pop();
            window.myBar.update();
        });

        document.getElementById('removeData').addEventListener('click', function () {
            barChartData.labels.splice(-1, 1); // remove the label first

            barChartData.datasets.forEach(function (dataset) {
                dataset.data.pop();
            });

            window.myBar.update();
        });
    </script>

</asp:Content>
