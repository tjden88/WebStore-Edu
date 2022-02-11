Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        removeFromCartLink: ""
    },

    init: function (properties) {
        $.extend(Cart._properties, properties);

        $(".add-to-cart").click(Cart.addToCart);

        $(".cart_quantity_up").click({ quantity: 1 }, Cart.changeItemCount);
        $(".cart_quantity_down").click({ quantity: -1 }, Cart.changeItemCount);
        $(".cart_quantity_delete").click(Cart.removeItem);

    },

    changeItemCount: function (event) {
        event.preventDefault();
        var htmlElement = $(this);

        const id = htmlElement.data("id");

        const quantity = event.data.quantity;

        var tr = htmlElement.closest("tr");

        const query = Cart._properties.addToCartLink + "/" + id + "?Quantity=" + quantity;

        $.get(query)
            .done(function (response) {


                const itemCount = parseInt($(".cart_quantity_input", tr).val());

                var newCount = itemCount + quantity;
                if (newCount > 0) {
                    $(".cart_quantity_input", tr).val(newCount);
                    Cart.refreshPrice(tr);
                } else {
                    tr.remove();
                    Cart.refreshTotalPrice();
                }
                Cart.refreshCartView();

                console.log(response.message + quantity);
            })
            .fail(function () { console.log("changeItemCount Fail!"); });
    },

    removeItem: function (event) {
        event.preventDefault();
        var htmlElement = $(this);

        const id = htmlElement.data("id");

        const query = Cart._properties.removeFromCartLink + "/" + id;

        $.get(query)
            .done(function (response) {
                htmlElement.closest("tr").remove();

                Cart.refreshTotalPrice();
                Cart.refreshCartView();

                console.log(response.message);
            })
            .fail(function () { console.log("AddToCart Fail!"); });
    },

    addToCart: function (event) {
        event.preventDefault();
        var htmlElement = $(this);

        const id = htmlElement.data("id");
        const quantity = $("#cart-add-quantity").val() ?? 1;

        const query = Cart._properties.addToCartLink + "/" + id + "?Quantity=" + quantity;

        $.get(query)
            .done(function (response) {
                Cart.showTooltip(htmlElement);
                Cart.refreshCartView();
                console.log(response.message);
            })
            .fail(function () { console.log("AddToCart Fail!"); });
    },

    showTooltip: function (htmlElement) {
        htmlElement.tooltip({ title: "Товар добавлен в корзину" }).tooltip("show");
        setTimeout(function () {
            htmlElement.tooltip("destroy");
        },
            1000);
    },

    refreshCartView: function () {
        $.get(Cart._properties.getCartViewLink)
            .done(function (cartHtml) {
                $("#cart-container").html(cartHtml);
            })
            .fail(function () { console.log("getCartViewLink Fail!"); });
    },

    refreshPrice: function (tr) {
        const itemCount = parseInt($(".cart_quantity_input", tr).val());
        const price = parseFloat($(".cart_price", tr).data("price"));

        const totalPrice = itemCount * price;

        var value = totalPrice.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

        const cartPrice = $(".cart_total_price", tr);
        cartPrice.html(value);
        cartPrice.data("price", totalPrice);

        Cart.refreshTotalPrice();
    },

    refreshTotalPrice: function () {
        var total = 0;

        $(".cart_total_price").each(function () {
            const price = parseFloat($(this).data("price"));
            total += price;

            var value = total.toLocaleString("ru-RU", { style: "currency", currency: "RUB" });

            $("#total-order-price").html(value);
        });
    }
}