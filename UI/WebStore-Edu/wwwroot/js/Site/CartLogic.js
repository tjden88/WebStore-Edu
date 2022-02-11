Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: "",
        removeFromCartLink: ""
    },

    init: function(properties) {
        $.extend(Cart._properties, properties);

        $(".add-to-cart").click(Cart.addToCart);

    },

    addToCart: function(event) {
        event.preventDefault();
        var htmlElement = $(this);

        const id = htmlElement.data("id");
        const quantity = $("#cart-add-quantity").val() ?? 1;

        const query = Cart._properties.addToCartLink + "/" + id + "?Quantity=" + quantity;

        $.get(query)
            .done(function(response) {
                Cart.showTooltip(htmlElement);
                Cart.refreshCartView();
                console.log(response.message);
            })
            .fail(function() { console.log("AddToCart Fail!"); });
    },

    showTooltip: function(htmlElement) {
        htmlElement.tooltip({ title: "Товар добавлен в корзину" }).tooltip("show");
        setTimeout(function() {
                htmlElement.tooltip("destroy");
            },
            1000);
    },

    refreshCartView: function() {
        $.get(Cart._properties.getCartViewLink)
            .done(function(cartHtml) {
                $("#cart-container").html(cartHtml);
            })
            .fail(function () { console.log("getCartViewLink Fail!"); });
    }
}