<script lang="ts">
  interface ButtonProps {
    text?: string;
    color?: ThemeColor;
    startIcon?: string;
    iconButton?: boolean;
    className?: string;
    style?: string;
    disabled?: boolean;
    command?: CallableFunction;
    commandParams?: any | any[];
  }

  let { color = "primary", ...props }: ButtonProps = $props();
</script>

<button
  class="py-2 px-4 text-sm font-[500] cursor-pointer rounded-full {props.iconButton
    ? ''
    : `btn-${color}`} {props.disabled
    ? 'btn-disabled'
    : ''} {props.className} flex items-center gap-1"
  style={props.style}
  disabled={props.disabled}
  onclick={(_) =>
    props.command?.(
      props.commandParams?.length > 0
        ? { ...props.commandParams }
        : props.commandParams
    )}
>
  {#if props.startIcon}
    <i
      class="{props.iconButton
        ? `text-6xl color-${color}`
        : 'text-xl'} {props.startIcon}"
    ></i>
  {/if}

  {props.text}
</button>
