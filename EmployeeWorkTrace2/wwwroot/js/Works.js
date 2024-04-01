

$(document).ready(function () {
    if (window.location.pathname.toLowerCase().includes('/myworks')) {
        // We are on the "My Works" page
        loadDataTableWithFilter();
    } else {
        // We are on any other works page
        loadDataTable();
    }
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "paging": true,
        "ajax": {
            url: '/worker/works/getall'},
        "columns": [
            { data: 'workId', "width": "5%" },
            { data: 'workName', "width": "15%" },
            { data: 'workerName', "width": "10%" },
            { data: 'workCreationDate', "width": "20%" },
            { data: 'workEndDate', "width": "20%" },
            {
                data: 'workId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Worker/Works/View/${data}" class="btn btn-primary mx-2"> 
                        <i class="bi bi-binoculars"></i> View 
                    </a>
                    <a href="/Worker/Works/Edit/${data}" class="btn btn-warning mx-2">
                        <i class="bi bi-pencil-square"></i> Edit 
                    </a>
                </div>`
                },
                "width": "20%"
            }

        ]
    });

}

function loadDataTableWithFilter() {
    dataTable = $('#tblData').DataTable({
        "paging": true,
        "ajax": {
            url: '/worker/works/getall',
            data: { myWorks: true }
        },
        "columns": [
            { data: 'workId', "width": "5%" },
            { data: 'workName', "width": "15%" },
            { data: 'workerName', "width": "10%" },
            { data: 'workCreationDate', "width": "20%" },
            { data: 'workEndDate', "width": "20%" },
            {
                data: 'workId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/Worker/Works/View/${data}" class="btn btn-primary mx-2"> 
                        <i class="bi bi-binoculars"></i> View 
                    </a>
                    <a href="/Worker/Works/Edit/${data}" class="btn btn-warning mx-2">
                        <i class="bi bi-pencil-square"></i> Edit 
                    </a>
                </div>`
                },
                "width": "20%"
            }

        ]
    });

}

