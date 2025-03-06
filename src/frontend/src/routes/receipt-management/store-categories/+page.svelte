<script lang="ts">
  import { onMount } from "svelte";
  import { page } from "$app/state";

  import {
    storeCategoryStore,
    fetchStoreCategories,
    createStoreCategory,
    deleteStoreCategory,
  } from "$lib/stores/receipt-management/store-category-store";

  import { toastStore, addToast } from "$lib/stores/toast-store";

  import Button from "@components/core/button/button.svelte";
  import TextInput from "@components/core/text-input/text-input.svelte";

  let dialogCreate: HTMLDialogElement = undefined;

  onMount(async () => {
    await fetchStoreCategories();

    dialogCreate = document.getElementById(
      "dialog-create"
    ) as HTMLDialogElement;
  });

  let formName: string = $state("");

  const openCreateForm = () => {
    formName = "";

    dialogCreate.showModal();
  };

  const closeCreateForm = async (submit: boolean) => {
    if (submit) {
      try {
        await createStoreCategory({
          name: formName,
        });

        dialogCreate.close();

        addToast({
          message: "Store category created",
          severity: "success",
        });
      } catch (err) {
        addToast({
          message: err,
          severity: "danger",
        });
      }
    } else {
      dialogCreate.close();
    }
  };
</script>

<div class="flex flex-row justify-between items-end mb-2">
  <div class="prose">
    <h1 class="">Store Categories</h1>
  </div>
  <Button text="Add" color="primary" command={openCreateForm} />
</div>
<hr class="mb-3" />

<dialog
  id="dialog-create"
  class="z-2 place-self-center backdrop:backdrop-blur rounded p-6 bg-neutral-light-base"
>
  <div class="prose">
    <h2>Add a Store Category</h2>
  </div>
  <hr class="mt-1 mb-3" />

  <div class="mb-3">
    <TextInput
      className="w-full"
      label="Name"
      bind:value={formName}
      placeholder="Enter a name..."
      required
      maxLenth={50}
      helperText="The name must be unique, and contain between 1 and 50 characters."
    />
  </div>
  <div class="flex flex-row gap-1 justify-end items-center">
    <Button
      text="Close"
      color="none"
      command={closeCreateForm}
      commandParams={false}
    />
    <Button
      text="Save"
      color="primary"
      command={closeCreateForm}
      commandParams={true}
      disabled={!formName || formName.length > 50}
    />
  </div>
</dialog>

{#if $storeCategoryStore.length === 0}
  <div
    class="bg-primary-bg outline outline-primary rounded p-3 text-primary size-fit"
  >
    There are no store categories! Create some using the button in the top-right
    corner.
  </div>
{/if}

<div class="flex flex-col overflow-y-scroll">
  {#each $storeCategoryStore as storeCategory}
    <div
      class="bg-neutral-light-base p-3 outline outline-1 outline-neutral-light-line rounded flex flex-row items-center justify-between w-auto m-1"
    >
      <div>{storeCategory.name}</div>
      <Button
        startIcon="bx bx-trash"
        text="Delete"
        color="danger"
        command={deleteStoreCategory}
        commandParams={storeCategory.id}
      />
    </div>
  {/each}
</div>
