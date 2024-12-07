var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#categoryTable').DataTable({
        "ajax": {
            url: '/admin/category/getall',
            dataSrc: 'data',
            error: function (response) {
                // Handle AJAX error here
                console.log('Error loading data:', response.responseJSON); // Log the error for debugging

                // Show an error alert
                Swal.fire({
                    title: 'Error!',
                    text: 'There was an issue loading the category data. Please try again later.',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
            }
        },
        "columns": [
            { data: 'id', "width": "15%" },
            { data: 'name', "width": "30%" },
            { data: 'displayOrder', "width": "15%" },
            {
                data: 'id',
                "width": "40%",
                "render": function (data) {
                    return `
                    <div class="flex flex-col justify-center md:flex-row items-center md:justify-start gap-2">
                       <a href="/Admin/Category/Update/${data}" class="black-ash-gradient flex items-center justify-center gap-2 rounded-md px-4 py-2">
                           <i class="fa-regular fa-pen-to-square text-white"></i>
                           <span  class="text-white">Update</span>
                       </a>
                       <button type="button" onClick=Delete('/Admin/Category/Delete/${data}') class="flex items-center justify-center gap-2 rounded-md px-4 py-2 shadow-md cursor-pointer" style="background-color: #d32f2f;">
                            <i class="fa-solid fa-trash-can text-white"></i>
                            <span class="text-white">Delete</span>
                       </button>
                   </div>
                `;
                }
            }
        ],
        "createdRow": function (row, data, dataIndex) {
            // Example: Apply inline style for custom padding
            $('td', row).css({
                'padding': '10px',
                'text-align': 'center',
                'border': '1px solid #ddd'
            });
        }
    });

}


function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        Swal.fire("Deleted!", data.message, "success");
                        dataTable.ajax.reload();
                    } else {
                        Swal.fire("Error!", data.message, "error");
                    }
                },
                error: function (response) {
                    console.log("Error occured during deletion: ", response?.responseJSON)
                    if (response?.responseJSON?.error) {
                        Swal.fire("Error!", response?.responseJSON?.error, "error");
                    } else {
                        Swal.fire("Error!", "Something went wrong!, please try again later.", "error");
                    }
                }
            });
        }
    });
}