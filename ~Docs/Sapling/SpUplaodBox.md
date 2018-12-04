## SpUplaodBox Component

### Front-End

**props**:

- config:	Set a link to load the configuration.
  ​		The response must be a ***JSend*** packet which defined ***StatUrl***, ***PreviewUrl***, ***SubmitUrl***.
- allow:	Determines what type of file is allowed to upload.
- caption:	The caption will display on a striking place.
- tag:		The tag will be send to the backend while the component doing anything.
- submit-text:	Determines the text of *submit* button.
- select-text:	Determines the text of *Select file* button.

**events**:

- submitted:	If the preview confirms, the event will be called.

**template**:

- slot-scope:	The parameter is match with ***StatUrl*** response from backend.

```html
<sp-upload-box caption="Caption" allow="jpg|png" tag="none"
               submit-text="Import" select-text="Select file"
               config="@Url.Action("Config", "SpUploadBox")"
               v-on:submitted="on_upload">
    <template slot-scope="{scope}">
        <span>{{scope}} Records</span>
    </template>
</sp-upload-box>
```

```html
<script>
    new Vue({
        el: '#app',
        methods:{
            on_upload: function() {
                window.location.reload();
            }
        }
    })
</script>
```



### Backend

Usually, you can simply create a new class through extend ***SpUploadBoxController***.

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

