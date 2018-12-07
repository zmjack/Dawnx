## SpUplaodBox Component

### Front-End

**Props**:

- **config**:			Set a link to load the configuration.
  ​				The response must be a **JSend** packet which defined ***StatUrl***, ***PreviewUrl***, ***SubmitUrl***.
- **event**:			Bind a **Vue** instance to listen events.
- tag:				The tag will be send to the backend while the component doing anything.
- allow:			Determines what type of file is allowed to upload.
- caption:			The caption will display on a striking place.
- submit-text:		Determines the text of *submit* button.
- select-text:		Determines the text of *Select file* button.

**On events**:

- submitted:		If the preview confirms, the event will be called.

**Emit events | Calls**:

- refresh:			Refresh stat info.

**Slots**:

- slot-scope:	The parameters is match with ***StatUrl*** response from backend.

**Template**:

```html
<!--if need to listen for events-->
<script>
    var eventVue = new Vue({
        created: function () {
            // emit events
            this.$emit(def_sp_upload_box + ".refresh");
            
            // on events
            this.$on(def_sp_upload_box + ":submitted", function (vue, data) {});
        }
    })
</script>
```

```html
<sp-upload-box config="@Url.Action("Config", "SpUploadBox")"
               caption="Caption" allow="jpg|png" tag="none"
               submit-text="Import" select-text="Select file"
               event="eventVue">
    <template slot-scope="scope">
        <span>{{scope.data.count}} Records</span>
    </template>
</sp-upload-box>
```



### Backend

Usually, you can simply create a new class through extend ***SpUploadBoxController***.

```C#
public class SpUploadBoxController : Sapling.SpUploadBoxController
{
    public override JsonResult Stat(string tag)
    {
        var count = new Random().Next();
        return Json(JSend.Success.Create(new { count }));
    }

    public override IActionResult Preview(string tag)
    {
		return View();
    }

    [HttpPost]
    public override JsonResult Submit(string tag)
    {
		return Json(JSend.Success.Create());
    }
}
```

The abstract class is defined as:

```C#
public abstract class SpUploadBoxController : Controller
{
	public virtual JsonResult Config()
    {
    	return Json(JSend.Success.Create(new SpUploadBox.Config
		{
			StatUrl = Url.Action(nameof(Stat)),
			PreviewUrl = Url.Action(nameof(Preview)),
			SubmitUrl = Url.Action(nameof(Submit)),
		}));
	}

	public abstract JsonResult Stat(string tag);
	public abstract ViewResult Preview(string tag);
	public abstract JsonResult Submit(string tag);
}
```

