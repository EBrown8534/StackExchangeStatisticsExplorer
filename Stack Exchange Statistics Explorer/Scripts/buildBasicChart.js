function buildBasicChart(m, fW, fH, siteId, chartSelector, apiField, fieldFormat, axisTitle, xTickLimit, filter) {
    d3.tsv("/API/1.0/SingleSiteField.ashx?Site=" + siteId + "&Field=" + apiField + "&DateFormat=d-MMM-yy&FieldFormat=" + fieldFormat + "&FileType=tsv", type, function (error, data) {
        if (filter != undefined && filter != null) {
            data = data.filter(filter);
        }

        var w = fW - m.left - m.right;
        var h = fH - m.top - m.bottom;

        var x = d3.scale.ordinal()
            .domain(data.map(function (d) { return d.Gathered; }))
            .rangeBands([0, w]);

        var y = d3.scale.linear()
            .range([h, 0]);

        var xAxis = d3.svg.axis()
            .scale(x)
            .tickValues(x.domain().filter(function (d, i) { return !(i % Math.floor(data.length / xTickLimit)); }))
            .orient("bottom");

        var yAxis = d3.svg.axis()
            .scale(y)
            .orient("left")
            .ticks(10);

        var chart = d3.select(chartSelector)
            .attr("width", w + m.left + m.right)
            .attr("height", h + m.top + m.bottom)
            .append("g")
            .attr("transform", "translate(" + m.left + "," + m.top + ")");

        var line = d3.svg.line()
            .x(function (d) { return x(d.Gathered); })
            .y(function (d) { return y(d.FieldValue); })
            .interpolate("monotone");

        y.domain([d3.min(data, function (d) { return d.FieldValue; }) - 0.001, d3.max(data, function (d) { return d.FieldValue; }) + 0.001]).nice();

        chart.append("g")
            .attr("class", "x axis lineChartAxis")
            .attr("transform", "translate(0," + h + ")")
            .call(xAxis)
            .selectAll("text")
                .style("text-anchor", "end")
                .attr("dx", "-.8em")
                .attr("dy", ".15em")
                .attr("transform", "rotate(-65)");

        chart.append("g")
            .attr("class", "y axis")
            .call(yAxis)
            .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", ".71em")
            .style("text-anchor", "end")
            .text(axisTitle);

        chart.append("path")
            .datum(data)
            .attr("class", "line")
            .attr("d", line)
            .attr("transform", "translate(" + (w / data.length / 2) + ",0)");
    });
}

function type(d) {
    d.Gathered = d.Gathered;
    d.FieldValue = +(d.FieldValue.replace("%", ""));
    return d;
}