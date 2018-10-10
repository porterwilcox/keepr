<template>
    <v-dialog v-model="showProfile" fullscreen transition="slide-y-transition">
        <v-btn @click="getUsersKsAndVs()" slot="activator" flat fab>
            <v-icon>fas fa-user</v-icon>
        </v-btn>
        <v-toolbar app color="amber darken-4">
            <v-spacer></v-spacer>
            <v-btn @click="closeProfile()" fab flat>
                <v-icon>close</v-icon>
            </v-btn>
        </v-toolbar>
        <v-container id="h2" fill-height fluid class="blue-grey lighten-5">
            <v-layout fill-height align-start column>
                <h1 class="mt-5 pa-1">{{user.username}}</h1>
                <v-expansion-panel class="mt-3">
                    <v-expansion-panel-content id="h-exp" class="blue-grey lighten-4 mb-3">
                        <h3 slot="header" class="pa-2">you've authored {{usersKeeps.length}} keeps</h3>
                        <v-layout fill-height align-center wrap>
                            <v-flex xs12 sm12 md2 lg3 pa-1 v-for="keep in usersKeeps" :key="keep.id">
                                <keeps :keep="keep" />
                            </v-flex>
                        </v-layout>
                    </v-expansion-panel-content>
                </v-expansion-panel>
                <h2 class="pa-3 mt-5">your vaults</h2>
                <v-layout fill-height justify-start align-start wrap>
                    <v-flex pa-1 v-for="(vault, index) in usersVaults" :key="vault.id">
                        <vaults :vault="vault" :i="index + 3" />
                    </v-flex>
                    <v-flex pa-1>
                        <v-card color="amber darken-4" width="20vw" height="10vw">
                            <v-layout v-if="!createVault" fill-height column align-center justify-center>
                                <h1>Create a Vault</h1>
                                <v-btn @click="createVault = !createVault" icon color="blue-grey lighten-4" small>
                                    <v-icon large color="">fas fa-plus-circle</v-icon>
                                </v-btn>
                            </v-layout>
                            <v-layout v-else fill-height column align-center justify-space-between>
                                <form class="mt-3">
                                    <v-text-field color="cyan lighten-3" single-line clearable v-model="newVault.name" label="name"></v-text-field>                                    
                                </form>
                                <div>
                                    <v-btn @click="createVault = !createVault" fab small color="amber darken-3">
                                        <v-icon>close</v-icon>
                                    </v-btn>
                                </div>
                            </v-layout>
                        </v-card>
                    </v-flex>
                </v-layout>
            </v-layout>
        </v-container>
    </v-dialog>
</template>
<script>
    import Keeps from "@/components/Keeps";
    import Vaults from "@/components/Vaults";
    export default {
        name: "profile",
        data() {
            return {
                showProfile: false,
                createVault: false,
                newVault: {
                    name: ""
                }
            };
        },
        computed: {
            user() {
                return this.$store.state.user;
            },
            usersKeeps() {
                return this.$store.state.usersKeeps;
            },
            usersVaults() {
                return this.$store.state.usersVaults;
            }
        },
        components: {
            Keeps,
            Vaults
        },
        methods: {
            getUsersKsAndVs() {
                this.$store.dispatch("getUsersKeeps", this.user.id);
                this.$store.dispatch("getUsersVaults", this.user.id);
            },
            closeProfile() {
                this.showProfile = false;
                this.$store.commit("setUsersKeeps", []);
            }
        }
    };
</script>
<style>
    #h2 {
        overflow-y: scroll;
    }

    #h-exp {
        height: fit-content;
        z-index: 2;
    }
</style>