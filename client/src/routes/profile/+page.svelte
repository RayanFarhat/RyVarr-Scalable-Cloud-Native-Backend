<script lang="ts">
  import { clearAccountData, getAccountData } from "../../core/accountData";
  import { browser } from "$app/environment";
  import { gotoURL } from "../../core/gotoURL";

  let username: string = "username";
  let isPro: boolean = true;

  let accountdata = { username: username, isPro: isPro };
  if (browser) {
    accountdata = getAccountData();
  }
  console.log(accountdata);

  if (accountdata.username != "") {
    username = accountdata.username;
    isPro = accountdata.isPro;
  }
  function onSignout() {
    if (browser) {
      clearAccountData();
      gotoURL("/login");
    }
  }
</script>

<br />
<div class="flex justify-center mt-10">
  <div class="card bg-base-100 shadow-xl">
    <div class="card-body">
      <h2 class="card-title">
        Hey {username}
        {#if isPro == true}
          <div class="badge badge-secondary">PRO</div>
        {/if}
      </h2>
      {#if isPro == true}
        <p>
          You are a professional user and has the freedom to utilize the
          company's products without any restrictions.
        </p>
      {:else}
        <p>
          You are a not professional user and can't use the company's products.
        </p>
      {/if}

      <div class="card-actions justify-end">
        <div class="badge badge-outline">HamzaCAD</div>
      </div>
      <button on:click={onSignout} class="btn btn-outline btn-primary"
        >Sign out</button
      >
    </div>
  </div>
</div>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
