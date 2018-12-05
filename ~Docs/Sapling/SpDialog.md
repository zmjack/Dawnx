## SpDialog

### Front-End

This component must be surround with 'uk-modal':

```html
<button type="button" data-uk-modal="{target:'#modal'}">Open</button>
<div id="modal" class="uk-modal">
    <sp-dialog>
        <template slot="header">This is header.</template>
        <template slot="body">This is body.</template>
        <template slot="footer">This is footer.</template>
    </sp-dialog>
</div>
```



### No Backend

