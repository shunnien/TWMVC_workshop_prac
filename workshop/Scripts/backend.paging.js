$(function ()
{

    $("form.form-search")
        .each(function (i, el)
        {
            var $form = $(this),
                $submit = $form.find("[type=submit]"),
                $column = $form.find("[name=Column]"),
                $sort = $form.find("[name=Sort]"),
                $paging = $form.parents(".paging"),
                $sortable = $paging.find(".sortable[data-column]"),
                $pager = $paging.find(".pagination");

            $sortable.click(function ()
            {
                var $this = $(this),
                    column = $this.data("column");

                if ($column.val() == column)
                {
                    if ($sort.val() == "Ascending")
                        $sort.val("Descending");
                    else
                        $sort.val("Ascending");
                }
                else
                {
                    $sort.val("Ascending");
                    $column.val(column);
                }

                $form.trigger("submit");

                return false;
            });

            $submit.click(function ()
            {
                $form.find("[name=Page]").val(1);
                $form.trigger("submit");
            });

            $pager.on("click", "a", function (evt)
            {
                var $page = $form.find("[name=Page]"),
                    page = parseInt(this.href.match(/(\d+)$/)[1]);

                if (isNaN(page))
                    return false;

                $page.val(page);
                $form.trigger("submit");

                return false;
            });
        });
});
