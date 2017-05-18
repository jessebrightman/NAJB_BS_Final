//Bind Job Name chips to Model

var JobNameChips = new Array();
var JobName = '';
//$('#JobNameChip').on('itemAdded', function () {
//    JobName = '';
//    JobNameChips.length = 0;
//    $('#JobNameChip .tag').each(function () {

//        JobNameChips.push($(this).text().replace('close', ''));
//        JobName = JobNameChips.join(', ');

//    });
//    $('#JobName').val(JobName);
//    //alert(JobName);
//});

$('#JobNameChip input').on('focusout', function () {
    var jobTags = $("#JobNameChip input").tagit("assignedTags");
    alert(jobTags);
});


$('#JobNameChip').on('itemRemoved', function (e, chip) {
    // you have the deleted chip here
    JobName = '';
    JobNameChips.length = 0;
    $('#JobNameChip .tag').each(function () {

        JobNameChips.push($(this).text().replace('close', ''));
        JobName = JobNameChips.join(', ');

    });
    $('#JobName').val(JobName);
    //alert(JobName);
});

//Bind Industry Name chips to Model

var IndustryNameChips = new Array();
var IndustryName = '';
$('#IndustryNameChip').on('itemAdded', function () {
    IndustryName = '';
    IndustryNameChips.length = 0;
    $('#IndustryNameChip .tag').each(function () {

        IndustryNameChips.push($(this).text().replace('close', ''));
        IndustryName = IndustryNameChips.join(', ');

    });
    $('#Industry').val(IndustryName);
    //alert(JobName);
});

$('#IndustryNameChip').on('itemRemoved', function (e, chip) {
    // you have the deleted chip here
    IndustryName = '';
    IndustryNameChips.length = 0;
    $('#IndustryNameChip .tag').each(function () {

        IndustryNameChips.push($(this).text().replace('close', ''));
        IndustryName = IndustryNameChips.join(', ');

    });
    $('#Industry').val(IndustryName);
    //alert(JobName);
});

//Bind Company Name chips to Model

var CompanyNameChips = new Array();
var CompanyName = '';
$('#CompanyNameChip').on('itemAdded', function () {
    CompanyName = '';
    CompanyNameChips.length = 0;
    $('#CompanyNameChip .tag').each(function () {

        CompanyNameChips.push($(this).text().replace('close', ''));
        CompanyName = CompanyNameChips.join(', ');

    });
    $('#CompanyName').val(CompanyName);
    //alert(JobName);
});

$('#CompanyNameChip').on('itemRemoved', function (e, chip) {
    // you have the deleted chip here
    CompanyName = '';
    CompanyNameChips.length = 0;
    $('#CompanyNameChip .tag').each(function () {

        CompanyNameChips.push($(this).text().replace('close', ''));
        CompanyName = CompanyNameChips.join(', ');

    });
    $('#CompanyName').val(CompanyName);
    //alert(JobName);
});

//Bind Location Name chips to Model

var LocationName = '';
$('#LocationNameChip').on('itemAdded', function () {
    LocationName = '';
    $('#LocationNameChip .tag').each(function () {
        LocationName = $(this).text().replace('close', '');
    });
    $('#Location').val(LocationName);
    alert($('#Location').val());
});

$('#LocationNameChip').on('itemRemoved', function (e, chip) {
    // you have the deleted chip here
    $('#LocationNameChip .tag').each(function () {
        LocationName = '';
    });
    $('#Location').val(LocationName);
    alert($('#Location').val());
});

//Bind Job Type chips to Model

var jobTypeVals = [];
var jobType = '';
$(function () {
    $('#JobTypeCheckbox input').click(function () {

        $('#JobTypeCheckbox :checked').each(function () {
            jobTypeVals.push($(this).val());
            jobType = jobTypeVals.join(', ');
            $('#JobType').val(jobType);
        });
        alert(jobType);
        jobType = '';
        jobTypeVals.length = 0;
    });
});

//Bind Experience chips to Model

var experienceVals = [];
var experience = '';
$(function () {
    $('#ExperienceCheckbox input').click(function () {

        $('#ExperienceCheckbox :checked').each(function () {
            experienceVals.push($(this).val());
            experience = experienceVals.join(', ');
            $('#Experience').val(experienceVals);
        });
        alert(experience);
        experience = '';
        experienceVals.length = 0;
    });
});