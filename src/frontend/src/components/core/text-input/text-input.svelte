<script lang="ts">
  import type { HTMLInputTypeAttribute } from "svelte/elements";

  interface TextInputProps {
    value?: string;
    label?: string;
    type?: HTMLInputTypeAttribute;
    errorText?: string;
    helperText?: string;
    className?: string;
    style?: string;
    disabled?: boolean;
    placeholder?: string;
    minLength?: number;
    maxLenth?: number;
    required?: boolean;
  }

  let { value = $bindable(""), ...props }: TextInputProps = $props();
</script>

<div class={props.className} style={props.style}>
  {#if props.label}
    <label class="block mb-1" for="">
      {props.label}

      {#if props.required}
        <span class="text-danger">*</span>
      {/if}
    </label>
  {/if}

  <input
    type={props.type}
    disabled={props.disabled}
    minlength={props.minLength}
    maxlength={props.maxLenth}
    placeholder={props.placeholder}
    required={props.required}
    bind:value
    class="w-full {props.errorText ||
    (props.required && !value) ||
    (props.minLength && value?.length < props.minLength) ||
    (props.maxLenth && value?.length > props.maxLenth)
      ? 'error'
      : ''}"
  />

  {#if props.errorText}
    <span class="mt-1 block text-sm text-danger">{props.errorText}</span>
  {:else if props.helperText}
    <span class="mt-1 block text-sm text-dark-disabled">{props.helperText}</span
    >
  {/if}
</div>
