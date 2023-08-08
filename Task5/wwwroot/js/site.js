$('.generator-configuration-js').change(replaceTableData)

$('#random-seed-js').click(e => {
    e.preventDefault()
    $('#seed-js').val(getRandomInt(2_147_483_648))
    replaceTableData()
})

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

function getRandomInt(max) {
    return Math.floor(Math.random() * max);
  }