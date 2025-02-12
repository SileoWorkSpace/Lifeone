function generateDayWiseTimeSeries(e, t, a) {
    for (var r = 0, o = []; r < t; ) {
        var i = e,
            n = Math.floor(Math.random() * (a.max - a.min + 1)) + a.min;
        o.push([i, n]), (e += 864e5), r++;
    }
    return o;
}
function generateData(e, t, a) {
    for (var r = 0, o = []; r < t; ) {
        var i = Math.floor(750 * Math.random()) + 1,
            n = Math.floor(Math.random() * (a.max - a.min + 1)) + a.min,
            s = Math.floor(61 * Math.random()) + 15;
        o.push([i, n, s]), 864e5, r++;
    }
    return o;
}
function generateData1(e, t, a) {
    for (var r = 0, o = []; r < t; ) {
        var i = Math.floor(Math.random() * (a.max - a.min + 1)) + a.min,
            n = Math.floor(61 * Math.random()) + 15;
        o.push([e, i, n]), (e += 864e5), r++;
    }
    return o;
}
(Apex = {
    chart: { parentHeightOffset: 0, toolbar: { show: !1 } },
    grid: { padding: { left: 0, right: 0 } },
    colors: ["#5369f8", "#43d39e", "#f77e53", "#1ce1ac", "#25c2e3", "#ffbe0b"],
    tooltip: { theme: "dark", x: { show: !1 } },
    dataLabels: { enabled: !1 },
    xaxis: { axisBorder: { color: "#d6ddea" }, axisTicks: { color: "#d6ddea" } },
    yaxis: { labels: { offsetX: -10 } },
}),
    (function (e) {
        "use strict";
        function t() {}
        (t.prototype.initCharts = function () {
            var e = {
                chart: { height: 380, type: "line", zoom: { enabled: !1 }, toolbar: { show: !1 } },
                dataLabels: { enabled: !0 },
                stroke: { width: [3, 3], curve: "smooth" },
                series: [
                    { name: "High - 2018", data: [28, 29, 33, 36, 32, 32, 33] },
                    { name: "Low - 2018", data: [12, 11, 14, 18, 17, 13, 13] },
                ],
                grid: { row: { colors: ["transparent", "transparent"], opacity: 0.2 }, borderColor: "#e9ecef" },
                markers: { style: "inverted", size: 6 },
                xaxis: { categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"], title: { text: "Month" }, axisBorder: { color: "#d6ddea" }, axisTicks: { color: "#d6ddea" } },
                yaxis: { title: { text: "Temperature" }, min: 5, max: 40 },
                legend: { position: "top", horizontalAlign: "right", floating: !0, offsetY: -25, offsetX: -5 },
                responsive: [{ breakpoint: 600, options: { chart: { toolbar: { show: !1 } }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-line-1"), e).render();
            e = {
                chart: { height: 380, type: "line", shadow: { enabled: !1, color: "#bbb", top: 3, left: 2, blur: 3, opacity: 1 } },
                stroke: { width: 5, curve: "smooth" },
                series: [{ name: "Likes", data: [4, 3, 10, 9, 29, 19, 22, 9, 12, 7, 19, 5, 13, 9, 17, 2, 7, 5] }],
                xaxis: {
                    type: "datetime",
                    categories: [
                        "1/11/2000",
                        "2/11/2000",
                        "3/11/2000",
                        "4/11/2000",
                        "5/11/2000",
                        "6/11/2000",
                        "7/11/2000",
                        "8/11/2000",
                        "9/11/2000",
                        "10/11/2000",
                        "11/11/2000",
                        "12/11/2000",
                        "1/11/2001",
                        "2/11/2001",
                        "3/11/2001",
                        "4/11/2001",
                        "5/11/2001",
                        "6/11/2001",
                    ],
                },
                title: { text: "Social Media", align: "left", style: { fontSize: "14px", color: "#666" } },
                fill: { type: "gradient", gradient: { shade: "dark", gradientToColors: ["#43d39e"], shadeIntensity: 1, type: "vertical", opacityFrom: 1, opacityTo: 1, stops: [0, 100, 100, 100] } },
                markers: { size: 4, opacity: 0.9, colors: ["#50a5f1"], strokeColor: "#fff", strokeWidth: 2, style: "inverted", hover: { size: 7 } },
                yaxis: { min: -10, max: 40, title: { text: "Engagement" } },
                grid: { row: { colors: ["transparent", "transparent"], opacity: 0.2 }, borderColor: "#185a9d" },
                responsive: [{ breakpoint: 600, options: { chart: { toolbar: { show: !1 } }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-line-2"), e).render();
            e = {
                chart: { height: 380, type: "area", stacked: !0 },
                dataLabels: { enabled: !1 },
                stroke: { width: [3, 3, 3], curve: "smooth" },
                series: [
                    { name: "South", data: generateDayWiseTimeSeries(new Date("11 Feb 2019 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "North", data: generateDayWiseTimeSeries(new Date("11 Feb 2019 GMT").getTime(), 20, { min: 10, max: 20 }) },
                    { name: "Central", data: generateDayWiseTimeSeries(new Date("11 Feb 2019 GMT").getTime(), 20, { min: 10, max: 15 }) },
                ],
                legend: { position: "top", horizontalAlign: "left" },
                xaxis: { type: "datetime" },
            };
            new ApexCharts(document.querySelector("#apex-area"), e).render();
            e = {
                chart: { height: 380, type: "bar", toolbar: { show: !1 } },
                plotOptions: { bar: { horizontal: !1, endingShape: "rounded", columnWidth: "55%" } },
                dataLabels: { enabled: !1 },
                stroke: { show: !0, width: 2, colors: ["transparent"] },
                series: [
                    { name: "Net Profit", data: [44, 55, 57, 56, 61, 58, 63, 60, 66] },
                    { name: "Revenue", data: [76, 85, 101, 98, 87, 105, 91, 114, 94] },
                    { name: "Free Cash Flow", data: [35, 41, 36, 26, 45, 48, 52, 53, 41] },
                ],
                xaxis: { categories: ["Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"] },
                legend: { offsetY: -10 },
                yaxis: { title: { text: "$ (thousands)" } },
                grid: { row: { colors: ["transparent", "transparent"], opacity: 0.2 }, borderColor: "#f1f3fa" },
                tooltip: {
                    y: {
                        formatter: function (e) {
                            return "$ " + e + " thousands";
                        },
                    },
                },
            };
            new ApexCharts(document.querySelector("#apex-column-1"), e).render();
            e = {
                chart: { height: 380, type: "bar", toolbar: { show: !1 } },
                plotOptions: { bar: { dataLabels: { position: "top" } } },
                dataLabels: {
                    enabled: !0,
                    formatter: function (e) {
                        return e + "%";
                    },
                    offsetY: -30,
                    style: { fontSize: "12px", colors: ["#304758"] },
                },
                series: [{ name: "Inflation", data: [2.3, 3.1, 4, 10.1, 4, 3.6, 3.2, 2.3, 1.4, 0.8, 0.5, 0.2] }],
                xaxis: {
                    categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    position: "top",
                    labels: { offsetY: -18 },
                    axisBorder: { show: !1 },
                    axisTicks: { show: !1 },
                    crosshairs: { fill: { type: "gradient", gradient: { colorFrom: "#D8E3F0", colorTo: "#BED1E6", stops: [0, 100], opacityFrom: 0.4, opacityTo: 0.5 } } },
                    tooltip: { enabled: !0, offsetY: -35 },
                },
                fill: { gradient: { enabled: !1, shade: "light", type: "horizontal", shadeIntensity: 0.25, gradientToColors: void 0, inverseColors: !0, opacityFrom: 1, opacityTo: 1, stops: [50, 0, 100, 100] } },
                yaxis: {
                    axisBorder: { show: !1 },
                    axisTicks: { show: !1 },
                    labels: {
                        show: !1,
                        formatter: function (e) {
                            return e + "%";
                        },
                    },
                },
                title: { text: "Monthly Inflation in Argentina, 2002", floating: !0, offsetY: 350, align: "center", style: { color: "#444" } },
                grid: { row: { colors: ["transparent", "transparent"], opacity: 0.2 }, borderColor: "#f1f3fa" },
            };
            new ApexCharts(document.querySelector("#apex-column-2"), e).render();
            var t = {
                chart: { height: 380, type: "bar" },
                plotOptions: { bar: { horizontal: !0 } },
                dataLabels: { enabled: !1 },
                series: [{ data: [400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380] }],
                xaxis: { categories: ["South Korea", "Canada", "United Kingdom", "Netherlands", "Italy", "France", "Japan", "United States", "China", "Germany"] },
            };
            new ApexCharts(document.querySelector("#apex-bar-1"), t).render();
            e = {
                chart: { height: 380, type: "bar", stacked: !0, toolbar: { show: !1 } },
                plotOptions: { bar: { horizontal: !0, barHeight: "80%" } },
                dataLabels: { enabled: !1 },
                series: [
                    { name: "Males", data: [0.4, 0.65, 0.76, 0.88, 1.5, 2.1, 2.9, 3.8, 3.9, 4.2, 4, 4.3, 4.1, 4.2, 4.5, 3.9, 3.5, 3] },
                    { name: "Females", data: [-0.8, -1.05, -1.06, -1.18, -1.4, -2.2, -2.85, -3.7, -3.96, -4.22, -4.3, -4.4, -4.1, -4, -4.1, -3.4, -3.1, -2.8] },
                ],
                grid: { xaxis: { showLines: !1 } },
                yaxis: { min: -5, max: 5, title: {} },
                tooltip: {
                    shared: !1,
                    x: {
                        formatter: function (e) {
                            return e;
                        },
                    },
                    y: {
                        formatter: function (e) {
                            return Math.abs(e) + "%";
                        },
                    },
                },
                xaxis: {
                    categories: ["85+", "80-84", "75-79", "70-74", "65-69", "60-64", "55-59", "50-54", "45-49", "40-44", "35-39", "30-34", "25-29", "20-24", "15-19", "10-14", "5-9", "0-4"],
                    title: { text: "Percent" },
                    labels: {
                        formatter: function (e) {
                            return Math.abs(Math.round(e)) + "%";
                        },
                    },
                },
                legend: { offsetY: -10 },
                grid: { borderColor: "#f1f3fa" },
            };
            new ApexCharts(document.querySelector("#apex-bar-2"), e).render();
            e = {
                chart: { height: 380, type: "line" },
                stroke: { width: 3, curve: "smooth" },
                series: [
                    { name: "Team A", type: "area", data: [44, 55, 31, 47, 31, 43, 26, 41, 31, 47, 33] },
                    { name: "Team B", type: "line", data: [55, 69, 45, 61, 43, 54, 37, 52, 44, 61, 43] },
                ],
                fill: { type: "solid", opacity: [0.35, 1] },
                labels: ["Dec 01", "Dec 02", "Dec 03", "Dec 04", "Dec 05", "Dec 06", "Dec 07", "Dec 08", "Dec 09 ", "Dec 10", "Dec 11"],
                markers: { size: 0 },
                yaxis: [{ title: { text: "Series A" } }, { opposite: !0, title: { text: "Series B" } }],
                tooltip: {
                    shared: !0,
                    intersect: !1,
                    y: {
                        formatter: function (e) {
                            return void 0 !== e ? e.toFixed(0) + " points" : e;
                        },
                    },
                },
            };
            new ApexCharts(document.querySelector("#apex-mixed-1"), e).render();
            e = {
                chart: { height: 380, type: "line", padding: { right: 0, left: 0 }, stacked: !1, toolbar: { show: !1 } },
                stroke: { width: [0, 2, 4], curve: "smooth" },
                plotOptions: { bar: { columnWidth: "50%" } },
                series: [
                    { name: "Team A", type: "column", data: [23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30] },
                    { name: "Team B", type: "area", data: [44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43] },
                    { name: "Team C", type: "line", data: [30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39] },
                ],
                fill: { opacity: [0.85, 0.25, 1], gradient: { inverseColors: !1, shade: "light", type: "vertical", opacityFrom: 0.85, opacityTo: 0.55, stops: [0, 100, 100, 100] } },
                labels: ["01/01/2003", "02/01/2003", "03/01/2003", "04/01/2003", "05/01/2003", "06/01/2003", "07/01/2003", "08/01/2003", "09/01/2003", "10/01/2003", "11/01/2003"],
                markers: { size: 0 },
                legend: { offsetY: -10 },
                xaxis: { type: "datetime" },
                yaxis: { title: { text: "Points" } },
                tooltip: {
                    shared: !0,
                    intersect: !1,
                    y: {
                        formatter: function (e) {
                            return void 0 !== e ? e.toFixed(0) + " points" : e;
                        },
                    },
                },
                grid: { borderColor: "#f1f3fa" },
            };
            new ApexCharts(document.querySelector("#apex-mixed-2"), e).render();
            e = {
                chart: { height: 380, type: "line", stacked: !1, toolbar: { show: !1 } },
                dataLabels: { enabled: !1 },
                stroke: { width: [0, 0, 3] },
                series: [
                    { name: "Income", type: "column", data: [1.4, 2, 2.5, 1.5, 2.5, 2.8, 3.8, 4.6] },
                    { name: "Cashflow", type: "column", data: [1.1, 3, 3.1, 4, 4.1, 4.9, 6.5, 8.5] },
                    { name: "Revenue", type: "line", data: [20, 29, 37, 36, 44, 45, 50, 58] },
                ],
                xaxis: { categories: [2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016] },
                yaxis: [
                    { axisTicks: { show: !0 }, axisBorder: { show: !0, color: "#675db7" }, labels: { style: { color: "#675db7" } }, title: { text: "Income (thousand crores)" } },
                    { axisTicks: { show: !0 }, axisBorder: { show: !0, color: "#23b397" }, labels: { style: { color: "#23b397" }, offsetX: 10 }, title: { text: "Operating Cashflow (thousand crores)" } },
                    { opposite: !0, axisTicks: { show: !0 }, axisBorder: { show: !0, color: "#e36498" }, labels: { style: { color: "#e36498" } }, title: { text: "Revenue (thousand crores)" } },
                ],
                tooltip: {
                    followCursor: !0,
                    y: {
                        formatter: function (e) {
                            return void 0 !== e ? e + " thousand crores" : e;
                        },
                    },
                },
                grid: { borderColor: "#f1f3fa" },
                legend: { offsetY: -10 },
                responsive: [{ breakpoint: 600, options: { yaxis: { show: !1 }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-mixed-3"), e).render();
            e = {
                chart: { height: 380, type: "bubble", toolbar: { show: !1 } },
                dataLabels: { enabled: !1 },
                series: [
                    { name: "Bubble 1", data: generateData(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Bubble 2", data: generateData(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Bubble 3", data: generateData(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                ],
                fill: { opacity: 0.8, gradient: { enabled: !1 } },
                xaxis: { tickAmount: 12, type: "category" },
                yaxis: { max: 70 },
                grid: { borderColor: "#f1f3fa" },
                legend: { offsetY: -10 },
            };
            new ApexCharts(document.querySelector("#apex-bubble-1"), e).render();
            t = {
                chart: { height: 380, type: "bubble", toolbar: { show: !1 } },
                dataLabels: { enabled: !1 },
                series: [
                    { name: "Product 1", data: generateData1(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Product 2", data: generateData1(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Product 3", data: generateData1(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Product 4", data: generateData1(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                ],
                fill: { type: "gradient" },
                xaxis: { tickAmount: 12, type: "datetime", labels: { rotate: 0 } },
                yaxis: { max: 70 },
                legend: { offsetY: -10 },
                grid: { borderColor: "#f1f3fa" },
            };
            new ApexCharts(document.querySelector("#apex-bubble-2"), t).render();
            e = {
                chart: { height: 380, type: "scatter", zoom: { enabled: !1 } },
                series: [
                    {
                        name: "Sample A",
                        data: [
                            [16.4, 5.4],
                            [21.7, 2],
                            [25.4, 3],
                            [19, 2],
                            [10.9, 1],
                            [13.6, 3.2],
                            [10.9, 7.4],
                            [10.9, 0],
                            [10.9, 8.2],
                            [16.4, 0],
                            [16.4, 1.8],
                            [13.6, 0.3],
                            [13.6, 0],
                            [29.9, 0],
                            [27.1, 2.3],
                            [16.4, 0],
                            [13.6, 3.7],
                            [10.9, 5.2],
                            [16.4, 6.5],
                            [10.9, 0],
                            [24.5, 7.1],
                            [10.9, 0],
                            [8.1, 4.7],
                            [19, 0],
                            [21.7, 1.8],
                            [27.1, 0],
                            [24.5, 0],
                            [27.1, 0],
                            [29.9, 1.5],
                            [27.1, 0.8],
                            [22.1, 2],
                        ],
                    },
                    {
                        name: "Sample B",
                        data: [
                            [6.4, 13.4],
                            [1.7, 11],
                            [5.4, 8],
                            [9, 17],
                            [1.9, 4],
                            [3.6, 12.2],
                            [1.9, 14.4],
                            [1.9, 9],
                            [1.9, 13.2],
                            [1.4, 7],
                            [6.4, 8.8],
                            [3.6, 4.3],
                            [1.6, 10],
                            [9.9, 2],
                            [7.1, 15],
                            [1.4, 0],
                            [3.6, 13.7],
                            [1.9, 15.2],
                            [6.4, 16.5],
                            [0.9, 10],
                            [4.5, 17.1],
                            [10.9, 10],
                            [0.1, 14.7],
                            [9, 10],
                            [12.7, 11.8],
                            [2.1, 10],
                            [2.5, 10],
                            [27.1, 10],
                            [2.9, 11.5],
                            [7.1, 10.8],
                            [2.1, 12],
                        ],
                    },
                    {
                        name: "Sample C",
                        data: [
                            [21.7, 3],
                            [23.6, 3.5],
                            [24.6, 3],
                            [29.9, 3],
                            [21.7, 20],
                            [23, 2],
                            [10.9, 3],
                            [28, 4],
                            [27.1, 0.3],
                            [16.4, 4],
                            [13.6, 0],
                            [19, 5],
                            [22.4, 3],
                            [24.5, 3],
                            [32.6, 3],
                            [27.1, 4],
                            [29.6, 6],
                            [31.6, 8],
                            [21.6, 5],
                            [20.9, 4],
                            [22.4, 0],
                            [32.6, 10.3],
                            [29.7, 20.8],
                            [24.5, 0.8],
                            [21.4, 0],
                            [21.7, 6.9],
                            [28.6, 7.7],
                            [15.4, 0],
                            [18.1, 0],
                            [33.4, 0],
                            [16.4, 0],
                        ],
                    },
                ],
                xaxis: {
                    tickAmount: 10,
                    labels: {
                        formatter: function (e) {
                            return parseFloat(e).toFixed(1);
                        },
                    },
                },
                yaxis: { tickAmount: 7 },
                grid: { borderColor: "#f1f3fa" },
                legend: { offsetY: -10 },
                responsive: [{ breakpoint: 600, options: { chart: { toolbar: { show: !1 } }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-scatter-1"), e).render();
            e = {
                chart: { height: 380, type: "scatter", zoom: { type: "xy" } },
                series: [
                    { name: "Team 1", data: generateDayWiseTimeSeries(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Team 2", data: generateDayWiseTimeSeries(new Date("11 Feb 2017 GMT").getTime(), 20, { min: 10, max: 60 }) },
                    { name: "Team 3", data: generateDayWiseTimeSeries(new Date("11 Feb 2017 GMT").getTime(), 30, { min: 10, max: 60 }) },
                    { name: "Team 4", data: generateDayWiseTimeSeries(new Date("11 Feb 2017 GMT").getTime(), 10, { min: 10, max: 60 }) },
                    { name: "Team 5", data: generateDayWiseTimeSeries(new Date("11 Feb 2017 GMT").getTime(), 30, { min: 10, max: 60 }) },
                ],
                dataLabels: { enabled: !1 },
                grid: { borderColor: "#f1f3fa", xaxis: { showLines: !0 }, yaxis: { showLines: !0 } },
                legend: { offsetY: -10 },
                xaxis: { type: "datetime" },
                yaxis: { max: 70 },
                responsive: [{ breakpoint: 600, options: { chart: { toolbar: { show: !1 } }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-scatter-2"), e).render();
            e = {
                chart: { height: 320, type: "pie" },
                series: [44, 55, 41, 17, 15],
                labels: ["Series 1", "Series 2", "Series 3", "Series 4", "Series 5"],
                legend: { show: !0, position: "bottom", horizontalAlign: "center", verticalAlign: "middle", floating: !1, fontSize: "14px", offsetX: 0, offsetY: -10 },
                responsive: [{ breakpoint: 600, options: { chart: { height: 240 }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-pie-1"), e).render();
            e = {
                chart: { height: 320, type: "donut" },
                series: [44, 55, 41, 17, 15],
                legend: { show: !0, position: "bottom", horizontalAlign: "center", verticalAlign: "middle", floating: !1, fontSize: "14px", offsetX: 0, offsetY: -10 },
                labels: ["Series 1", "Series 2", "Series 3", "Series 4", "Series 5"],
                responsive: [{ breakpoint: 600, options: { chart: { height: 240 }, legend: { show: !1 } } }],
                fill: { type: "gradient" },
            };
            new ApexCharts(document.querySelector("#apex-pie-2"), e).render();
            e = {
                chart: { height: 320, type: "donut", dropShadow: { enabled: !0, color: "#111", top: -1, left: 3, blur: 3, opacity: 0.2 } },
                stroke: { show: !0, width: 2 },
                series: [44, 55, 41, 17, 15],
                labels: ["Comedy", "Action", "SciFi", "Drama", "Horror"],
                dataLabels: { dropShadow: { blur: 3, opacity: 0.8 } },
                fill: { type: "pattern", opacity: 1, pattern: { enabled: !0, style: ["verticalLines", "squares", "horizontalLines", "circles", "slantedLines"] } },
                states: { hover: { enabled: !1 } },
                legend: { show: !0, position: "bottom", horizontalAlign: "center", verticalAlign: "middle", floating: !1, fontSize: "14px", offsetX: 0, offsetY: -10 },
                responsive: [{ breakpoint: 600, options: { chart: { height: 240 }, legend: { show: !1 } } }],
            };
            new ApexCharts(document.querySelector("#apex-pie-3"), e).render();
            e = { chart: { height: 350, type: "radialBar" }, plotOptions: { radialBar: { hollow: { size: "70%" } } }, colors: ["#50a5f1"], series: [70], labels: ["CRICKET"] };
            new ApexCharts(document.querySelector("#apex-radialbar-1"), e).render();
            e = {
                chart: { height: 350, type: "radialBar" },
                plotOptions: {
                    radialBar: {
                        dataLabels: {
                            name: { fontSize: "22px" },
                            value: { fontSize: "16px" },
                            total: {
                                show: !0,
                                label: "Total",
                                formatter: function (e) {
                                    return 249;
                                },
                            },
                        },
                    },
                },
                series: [44, 55, 67, 83],
                labels: ["Apples", "Oranges", "Bananas", "Berries"],
            };
            new ApexCharts(document.querySelector("#apex-radialbar-2"), e).render();
            e = {
                chart: { height: 350, type: "radialBar" },
                plotOptions: {
                    radialBar: {
                        startAngle: -135,
                        endAngle: 135,
                        dataLabels: {
                            name: { fontSize: "16px", color: void 0, offsetY: 120 },
                            value: {
                                offsetY: 76,
                                fontSize: "22px",
                                color: void 0,
                                formatter: function (e) {
                                    return e + "%";
                                },
                            },
                        },
                    },
                },
                fill: { gradient: { enabled: !0, shade: "dark", shadeIntensity: 0.15, inverseColors: !1, opacityFrom: 1, opacityTo: 1, stops: [0, 50, 65, 91] } },
                stroke: { dashArray: 4 },
                colors: ["#f46a6a"],
                series: [67],
                labels: ["Median Ratio"],
                responsive: [{ breakpoint: 380, options: { chart: { height: 280 } } }],
            };
            new ApexCharts(document.querySelector("#apex-radialbar-3"), e).render();
        }),
            (t.prototype.init = function () {
                this.initCharts();
            }),
            (e.ApexChartPage = new t()),
            (e.ApexChartPage.Constructor = t);
    })(window.jQuery),
    (function () {
        "use strict";
        window.jQuery.ApexChartPage.init();
    })();
