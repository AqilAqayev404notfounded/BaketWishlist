<script>
    $(document).ready(function () {
        $('.btn-add-cart, .qty-right-plus').click(function (e) {
            e.preventDefault();

            var productId = $(this).closest('.product-box').data('product-id');

            $.ajax({
                url: '@Url.Action("AddToBasket", "Home")', 
                type: 'POST',
                data: { id: productId }, 
                success: function (response) {
                    alert('Ürün sepete eklendi!');
                },
                error: function (xhr, status, error) {
                    alert('Ürün sepete eklenirken bir hata oluştu!');
                }
            });
        });
    });
</script>
