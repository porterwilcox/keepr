<template>
    <v-dialog v-model="openMenu" persistent>
        <v-btn slot="activator" flat fab small color="amber darken-4">keep</v-btn>
        <v-layout id="dialog" row justify-center>
            <v-flex md6>
                <v-card>
                    <v-card-text>
                        <v-checkbox v-for="(vault, i) in usersVaults" :key="vault.id" :label="vault.name" 
                        :color="!(i % 3) ? 'light-blue darken-3' : !(i % 2) ? 'cyan darken-3' : 'teal darken-3'"
                        v-model="vaults[vault.id]"></v-checkbox>
                    </v-card-text>
                    <v-toolbar card>
                        <v-spacer></v-spacer>
                        <v-btn @click="openMenu = !openMenu" round color="cyan lighten-4">cancel</v-btn>
                        <v-btn @click="addKeepToVaults()" round color="amber darken-4">add to vault</v-btn>
                    </v-toolbar>
                </v-card>
            </v-flex> 
        </v-layout>
    </v-dialog>
</template>
<script>
export default {
  name: "addKeepToVault",
  props: ["keep"],
  data() {
    return {
      openMenu: false,
      vaults: {}
    };
  },
  computed: {
    usersVaults() {
      return this.$store.state.usersVaults;
    }
  },
  watch: {
    openMenu(value) {
      for (let i = 0; i < this.usersVaults.length; i++) {
        let v = this.usersVaults[i];
        this.$data.vaults[v.id] = false;
      }
    }
  },
  methods: {
    addKeepToVaults() {
      let vaultIds = Object.keys(this.vaults);
      let bools = Object.values(this.vaults);
      for (let i = 0; i < bools.length; i++) {
        if (bools[i]) {
          let vk = {
            userId: this.usersVaults[i].userId,
            vaultId: vaultIds[i],
            keepId: this.keep.id
          };
          this.$store.dispatch("addKeepToVault", vk);
        }
      }
      this.openMenu = false;
    }
  }
};
</script>
<style scoped>
#dialog -webkit-box-shadow {
  box-shadow: none !important;
}
</style>
