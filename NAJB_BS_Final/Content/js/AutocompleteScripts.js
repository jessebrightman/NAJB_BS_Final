$(document).ready(function () {
var JobNameChips = new Array();
var JobName = '';
    $("#JobNameChip input").tagit({
        autocomplete: {
            source: function (request, response) {
                $.ajax({
                    url: "/Ajax/AutoCompleteGetJobNameQuery",
                    type: "GET",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {

                        return { label: item.Title, value: item.Title };
                    }))

                }
            })
}
},
        allowSpaces: true,
        afterTagAdded: function (event, ui) {
            JobName = '';
            JobNameChips.length = 0;
            $('#JobNameChip .tagit-label').each(function ()
            {
                JobNameChips.push($(this).text());
                JobName = JobNameChips.join(', ');
            });
            $('#JobName').val(JobName);
        },
        afterTagRemoved: function (event, ui) {
            JobName = '';
            JobNameChips.length = 0;
            $('#JobNameChip .tagit-label').each(function ()
            {
                JobNameChips.push($(this).text());
                JobName = JobNameChips.join(', ');
            });
            $('#JobName').val(JobName);
        }
    });
});

$(document).ready(function () {
    var IndustryNameChips = new Array();
    var IndustryName = '';
    $("#IndustryNameChip input").tagit({
        autocomplete: {
            source: function (request, response) {
                $.ajax({
                    url: "/Ajax/AutoCompleteGetJobIndustryQuery",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { label: item.Industries, value: item.Industries };
                        }))

                    }
                })
            }
        },
        allowSpaces: true,
        afterTagAdded: function (event, ui) {
            IndustryName = '';
            IndustryNameChips.length = 0;
            $('#IndustryNameChip .tagit-label').each(function () 
            {
                IndustryNameChips.push($(this).text());
                IndustryName = IndustryNameChips.join(', ');
            });
            $('#Industry').val(IndustryName);
        },
        afterTagRemoved: function (event, ui) {
            IndustryName = '';
            IndustryNameChips.length = 0;
            $('#IndustryNameChip .tagit-label').each(function ()
            {
                IndustryNameChips.push($(this).text());
                IndustryName = IndustryNameChips.join(', ');
            });
            $('#Industry').val(IndustryName);
        }
    });
});

$(document).ready(function () {
    var CompanyNameChips = new Array();
    var CompanyName = '';
    $("#CompanyNameChip input").tagit({
        autocomplete: {
            source: function (request, response) {
                $.ajax({
                    url: "/Ajax/AutoCompleteGetCompanyNameQuery",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                            return { label: item.Companies, value: item.Companies };
                        }))

                    }
                })
            }
        },
        allowSpaces: true,
        afterTagAdded: function (event, ui) {
            CompanyName = '';
            CompanyNameChips.length = 0;
            $('#CompanyNameChip .tagit-label').each(function () {
                CompanyNameChips.push($(this).text());
                CompanyName = CompanyNameChips.join(', ');
            });
            $('#CompanyName').val(CompanyName);
        },
        afterTagRemoved: function (event, ui) {
            CompanyName = '';
            CompanyNameChips.length = 0;
            $('#CompanyNameChip .tagit-label').each(function () {
                CompanyNameChips.push($(this).text());
                CompanyName = CompanyNameChips.join(', ');
            });
            $('#CompanyName').val(CompanyName);
        }
    });
});

$(document).ready(function () {
    var LocationName = '';
    $("#LocationChip input").tagit({
        autocomplete: {
            source: function (request, response) {
                $.getJSON(
                    "https://secure.geobytes.com/AutoCompleteCity?key=7801e826329f5ba0cee7295cc2802047&callback=?&q=" + request.term,
                    //"http://gd.geobytes.com/AutoCompleteCity?key=7801e826329f5ba0cee7295cc2802047&callback=?&q=" + request.term,
                    function (data) {
                        response(data);
                    }
                );
            },
            minLength: 3,
        },
        tagLimit: 1,
        allowSpaces: true,
        afterTagAdded: function (event, ui) {
            LocationName = '';
            LocationName = ui.tagLabel;

            $('#Location').val(LocationName);
        },
        afterTagRemoved: function (event, ui) {
            LocationName = '';
            $('#Location').val(LocationName);
        }
    });
});

//Bind Additional Locations

$(function () {
    $('#RadiusRadioButton input').click(function () {
        var location = $('#Location').val();

        var radius1 = $('#Radiusch1').val();
        var radius2 = $('#Radiusch2').val();
        var radius3 = $('#Radiusch3').val();
        var radius4 = $('#Radiusch4').val();
        var radius = '';
        var thisRadius = $(this).val();

        locations = "";

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
                }
                $('#AdditionalLocations').val(locations);
                $('#Radius').val(thisRadius);
            });

        }
    });
});

$(document).ready(function () {
    var ApplyTo = '';
    $("#ApplyToChip input").tagit({
        afterTagAdded: function (event, ui) {
            ApplyTo = '';
            ApplyTo = ui.tagLabel;

            $('#Location').val(ApplyTo);
        },
        afterTagRemoved: function (event, ui) {
            ApplyTo = '';
            $('#Location').val(ApplyTo);
        }
    });
});

$(document).ready(function () {
    var Reference = '';
    $("#ReferenceChip input").tagit({
        afterTagAdded: function (event, ui) {
            Reference = '';
            Reference = ui.tagLabel;

            $('#ReferenceNumber').val(Reference);
        },
        afterTagRemoved: function (event, ui) {
            Reference = '';
            $('#ReferenceNumber').val(Reference);
        }
    });
});

