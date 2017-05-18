//Autocomplete Job Names

var jobNames = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: {
        url: 'https://localhost:44391/Ajax/AutoCompleteGetJobName',
        filter: function (list) {
            return $.map(list, function (Title) {
                console.log(list);
                return { name: Title };
            });
        }
    },
    remote: {
        url: "https://localhost:44391/Ajax/AutoCompleteGetJobNameQuery?term=%QUERY",
        filter: function (x) {
            console.log(x);
            return $.map(x, function (item) {
                return { name: item.Title };
            });
        },
        wildcard: "%QUERY"
    }
});
jobNames.initialize();

$('#JobNameChip input').tagsinput({
    typeaheadjs: {
        hint: false,
        name: 'jobNames',
        displayKey: 'name',
        valueKey: 'name',
        source: jobNames.ttAdapter(),
        afterSelect: function () {
                    this.$element[0].value = '';
                },
        confirmKeys: [13, 44, 9]
    }
});

//Autocomplete Industry Names

var industryNames = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: {
        url: 'https://localhost:44391/Ajax/AutoCompleteGetJobIndustry',
        filter: function (list) {
            return $.map(list, function (Title) {
                console.log(list);
                return { name: Title };
            });
        }
    },
    remote: {
        url: "https://localhost:44391/Ajax/AutoCompleteGetJobIndustryQuery?term=%QUERY",
        filter: function (x) {
            console.log(x);
            return $.map(x, function (item) {
                return { name: item.Title };
            });
        },
        wildcard: "%QUERY"
    }
});
industryNames.initialize();

$('#IndustryNameChip input').tagsinput({
    typeaheadjs: {
        hint: false,
        name: 'industryNames',
        displayKey: 'name',
        valueKey: 'name',
        source: industryNames.ttAdapter(),
        afterSelect: function () {
            this.$element[0].value = '';
        },
        confirmKeys: [13, 44, 9]
    }
});

//Autocomplete Company Names

var companyNames = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: {
        url: 'https://localhost:44391/Ajax/AutoCompleteGetCompanyName',
        filter: function (list) {
            return $.map(list, function (Title) {
                console.log(list);
                return { name: Companies };
            });
        }
    },
    remote: {
        url: "https://localhost:44391/Ajax/AutoCompleteGetCompanyNameQuery?term=%QUERY",
        filter: function (x) {
            console.log(x);
            return $.map(x, function (item) {
                return { name: item.Companies };
            });
        },
        wildcard: "%QUERY"
    }
});
companyNames.initialize();

$('#CompanyNameChip input').tagsinput({
    addOnBlur: true,
    typeaheadjs: {
        hint: false,
        name: 'companyNames',
        displayKey: 'name',
        valueKey: 'name',
        source: companyNames.ttAdapter(),
        afterSelect: function () {
            this.$element[0].value = '';
        },
        confirmKeys: [13, 44, 9]
    }
});

//Autocomplete Location

  var locationCity = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    //prefetch: {
    //    url: 'https://secure.geobytes.com/AutoCompleteCity?key=7801e826329f5ba0cee7295cc2802047&callback=?',
    //    filter: function (list) {
    //        return $.map(list, function (Title) {
    //            console.log(list);
    //            return { name: Companies };
    //        });
    //    }
    //},
    remote: {
        url: "https://secure.geobytes.com/AutoCompleteCity?key=7801e826329f5ba0cee7295cc2802047&callback=?&q=%QUERY",
        filter: function (x) {
            console.log(x);
            return $.map(x, function (item) {
                return { name: item };
            });
        },
        wildcard: "%QUERY"
    }
});
        locationCity.initialize();

$('#LocationChip input').tagsinput({
    addOnBlur: true,
    splitOn: null,
    confirmKeys: [13],
    allowDuplicates: false,
    maxTags: 1,
    typeaheadjs: {
        hint: false,
        delimiter: '|',
        name: 'locationCity',
        displayKey: 'name',
        valueKey: 'name',
        source: locationCity.ttAdapter(),
        afterSelect: function () {
            this.$element[0].value = '';
        }
    }
});

//Bind Additional Locations

$(function () {
    $('#RadiusRadioButton input').click(function () {
        var location = $('#Location').val();
        //alert(location);
        var radius1 = $('#Radiusch1').val();
        var radius2 = $('#Radiusch2').val();
        var radius3 = $('#Radiusch3').val();
        var radius4 = $('#Radiusch4').val();
        var radius = '';
        var thisRadius = $(this).val();

        locations = "";
        //alert(thisRadius);
        if (location != null && thisRadius != null) {
            $.getJSON("https://secure.geobytes.com/GetNearbyCities?callback=?&key=7801e826329f5ba0cee7295cc2802047&limit=100&radius=" + thisRadius + "&locationcode=" + location,
                //"http://getnearbycities.geobytes.com/GetNearbyCities?callback=?&limit=100&radius=" + radius + "&locationcode=" + location,
            function (data) {
                //alert(data);
                for (var i = 0; i < data.length; i++) {
                    addLocations(data[i]);
                }

                function addLocations(rowData) {
                    locations += (rowData[1] + ", " + rowData[2] + ", " + rowData[3] + ";");
                    //alert(locations);

                }
                $('#AdditionalLocations').val(locations);
                $('#Radius').val(thisRadius);
                //alert($('#AdditionalLocations').val());
                //alert($('#Radius').val());
            });

        }
    });
});

$('#DateChip input').tagsinput({
    maxTags: 1
});

$(".twitter-typeahead input").on('focusout', function () {
    var elem = $(this).closest(".bootstrap-tagsinput").parent().children("input").last();
    //console.log(elem);
    elem.tagsinput('add', $(this).val());
    $(this).typeahead('val', '');
});