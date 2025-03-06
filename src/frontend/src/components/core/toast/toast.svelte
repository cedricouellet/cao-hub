<script lang="ts">
  const props: ToastData = $props();
</script>

<div data-uuid={props.uuid} class="toast toast-{props.severity}">
  {props.message}
</div>
