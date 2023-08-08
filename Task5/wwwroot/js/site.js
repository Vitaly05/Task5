﻿//$('#generate-js-button').click(replaceTableData)
$('.generator-configuration-js').change(replaceTableData)

async function replaceTableData() {
    $.post('/getUsersData', getGeneratorConfiguration(), function (data) { 
        $('#users-table-body-js').html(data)
    })
}

function getGeneratorConfiguration() {
    return {
        locale: $('#locale-js').val(),
        seed: $('#seed-js').val()
    }
}