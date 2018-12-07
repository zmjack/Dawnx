## SpTable Component

### Front-End

**props**:

- **config**:			Set a link to load the configuration.
  ​				The response must be a **JSend** packet which defined ***SourceUrl***.
- **event**:			Binds a **Vue** instance to listen events.
- tag:				The tag will be send to the backend while the component doing anything.
- condensed:		Determines whether to reduce row spacing.
- check-name:		If set, a checkbox appears before each row, which is named as the value.
- col-widths:		Use an **Array** to set the width of each column.

**On events**:

- selectChange:	This event is called when column is checked or row is selected.

**Emit events | Calls**:

- refresh:			Reloads the current page.
- load:			Requests the specified page and display.
- freezeLeft:		Freezes the specified number of columns from the left side of the table.
- freezeRight:		Freezes the specified number of columns from the right side of the table.
- condense:		Sets to reduce row spacing or not.

**Slots**:

- loading:			There is a loading picture by default.
- top-menu:		This slot is displayed on the table.
  ​				*{ data: { pageNumber: 1, pageSize: 10, pageCount: 20}, call: function }*
- row-kit:			Row data.

**Template**

```html
<!--if need to listen for events-->
<script>
    var eventVue = new Vue({
        created: function () {
            // emit events
            this.$emit(def_sp_table + ".refresh");
            this.$emit(def_sp_table + ".load", 1);	// (page)
            this.$emit(def_sp_table + ".freezeLeft", 1);	// (count)
            this.$emit(def_sp_table + ".freezeRight", 1);	// (count)
            this.$emit(def_sp_table + ".condense", true);	// (count)
            
            // on events
         	this.$on(def_sp_table + ":selectChange", function (vue, selectedKeys){});
        }
    })
</script>
```

```html
<sp-table config="@Url.Action("Config", "SpTable")"
          condensed="true" check-name="emps" col-widths="[,,,]" event="eventVue">
    <template slot="loading">
        <div style="text-align:center">
            <img src="~/modules/sapling/images/Eclipse-1s-64px.gif" />
        </div>
    </template>
    <template slot="top-menu" slot-scope="scope">
        <div class="uk-float-left">
            <input type="number" v-on:change="scope.call('freeze')('left', $event.target.value)" />
            <input type="number" v-on:change="scope.call('freeze')('right', $event.target.value)" />
        </div>
        <div class="uk-float-right">
            <div class="uk-display-inline">
                <span>Page：{{scope.data.page}}/{{scope.data.pageCount}}</span>
                <button type="button" class="uk-button uk-button-mini" v-on:click="scope.call('load')(scope.data.page-1)">
                    <i class="uk-icon-toggle-left"></i> Previous
                </button>
                <button type="button" class="uk-button uk-button-mini" v-on:click="scope.call('load')(scope.data.page+1)">
                    Next <i class="uk-icon-toggle-right"></i>
                </button>
            </div>
            <div class="uk-display-inline">
                <span>Condensed：</span>
                <button type="button" class="uk-button uk-button-mini" v-on:click="scope.call('condense')(false)">Off</button>
                <button type="button" class="uk-button uk-button-mini" v-on:click="scope.call('condense')(true)">On</button>
            </div>
        </div>
    </template>
    <template slot="row-kit" slot-scope="{row}">
        <a v-bind:href="'@Url.Action("Edit")?id=' + row.key">Edit</a> |
        <a v-bind:href="'@Url.Action("Delete")?id=' + row.key">Delete</a>
    </template>
</sp-table>
```



### Backend

Usually, you can simply create a new class through extend ***SpTableController***.

```C#
public class SpTableController : Sapling.SpTableController
{
    private readonly EmployeesDbContext _context;

    public SpTableController(EmployeesDbContext context)
    {
        _context = context;
    }

    public override JsonResult Source(string tag, int page)
    {
        return Json(JSend.Success.Create(
            SpTable.NewSource(_context.Employees.SelectPage(page, 10))));
    }
}
```

The abstract class is defined as:

```C#
public abstract class SpTableController : Controller
{
    public virtual JsonResult Config()
    {
        return Json(JSend.Success.Create(new
        {
            sourceUrl = Url.Action(nameof(Source)),
        }));
    }

    public abstract JsonResult Source(string tag, int page);
}
```

